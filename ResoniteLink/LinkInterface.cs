using System;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
using System.IO;
using System.Text.Json;

namespace ResoniteLink
{
    public class LinkInterface : IDisposable
    {
        const int DEFAULT_BUFFER_SIZE = 1024 * 1024 * 2; // 2 MB

        public bool IsConnected => _client.State == WebSocketState.Open;
        public Exception FailureException { get; private set; }

        readonly ClientWebSocket _client = new ClientWebSocket();
        readonly SemaphoreSlim _clientWriteLock = new SemaphoreSlim(1);
        
        readonly CancellationTokenSource _receiverCts = new CancellationTokenSource();
        
        readonly JsonSerializerOptions _options = new JsonSerializerOptions()
        {
            // Necessary for values like Infinity, NaN and so on
            NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals,
            AllowOutOfOrderMetadataProperties = true,
        };

        readonly ConcurrentDictionary<string, TaskCompletionSource<Response>> _pendingResponses = new ConcurrentDictionary<string, TaskCompletionSource<Response>>();

        public async Task Connect(Uri target, CancellationToken cancellationToken)
        {
            using (await SemaphoreLock.AcquireAsync(_clientWriteLock, cancellationToken))
            {
                if (IsConnected || _receiverCts.IsCancellationRequested)
                    throw new InvalidOperationException("Client has already been initialized.");

                await _client.ConnectAsync(target, cancellationToken);
            }

            _ = Task.Run(ReceiverHandler);
        }

        async Task ReceiverHandler()
        {
            var cancellationToken = _receiverCts.Token;
            try
            {
                byte[] buffer = new byte[DEFAULT_BUFFER_SIZE];

                int receivedBytes = 0;

                while (!cancellationToken.IsCancellationRequested)
                {
                    if (receivedBytes == buffer.Length)
                    {
                        // We need bigger buffer!
                        var newBuffer = new byte[buffer.Length * 2];

                        Array.Copy(buffer, newBuffer, buffer.Length);

                        buffer = newBuffer;
                    }

                    var message = await _client.ReceiveAsync(new ArraySegment<byte>(buffer, receivedBytes, buffer.Length - receivedBytes), cancellationToken);

                    receivedBytes += message.Count;

                    if (!message.EndOfMessage)
                        continue;

                    switch(message.MessageType)
                    {
                        case WebSocketMessageType.Text:
                            var response = System.Text.Json.JsonSerializer.Deserialize<Response>(
                                new MemoryStream(buffer, 0, receivedBytes), _options);

                            if (_pendingResponses.TryRemove(response.SourceMessageID, out var completion))
                                completion.SetResult(response);
                            else
                                throw new Exception("There is no pending response with this ID");

                                break;

                        case WebSocketMessageType.Binary:
                            throw new NotSupportedException("Binary messages aren't currently supported");
                            break;

                        case WebSocketMessageType.Close:
                            _receiverCts.Cancel();
                            break;
                    }

                    receivedBytes = 0;
                }
            }
            catch(Exception ex)
            {
                FailureException = ex;
            }

            if(_client.State == WebSocketState.Open)
                await _client.CloseAsync(FailureException == null ?
                    WebSocketCloseStatus.NormalClosure :
                    WebSocketCloseStatus.InternalServerError, 
                    FailureException == null ? "Closing" : "Internal Error", CancellationToken.None);

            _client.Dispose();
        }

        async Task<O> SendMessage<I,O>(I message, CancellationToken cancellationToken = default)
            where I : Message
            where O : Response
        {
            EnsureMessageID(message);

            var responseCompletion = new TaskCompletionSource<Response>();

            var jsonData = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes((Message)message);

            using (await SemaphoreLock.AcquireAsync(_clientWriteLock, cancellationToken))
            {
                // Enqueue the message after the lock is acquired, so cancellation can prevent the response being ready.
                if (!_pendingResponses.TryAdd(message.MessageID, responseCompletion))
                    throw new InvalidOperationException(
                        "Failed to register MessageID. Did you provide duplicate MessageID?");

                await _client.SendAsync(new ArraySegment<byte>(jsonData),
                    WebSocketMessageType.Text, true, System.Threading.CancellationToken.None);

                if (message is BinaryPayloadMessage binaryPayload)
                {
                    // We must send the binary payload as well following the message
                    await _client.SendAsync(new ArraySegment<byte>(binaryPayload.RawBinaryPayload),
                        WebSocketMessageType.Binary, true,
                        System.Threading.CancellationToken.None);
                }
            }

            // Wait for response to arrive and cast it to the target type if compatible
            return await responseCompletion.Task as O;
        }

        void EnsureMessageID(Message message)
        {
            if (message.MessageID == null)
                message.MessageID = Guid.NewGuid().ToString();
        }

        #region API

        public Task<SessionData> GetSessionData(CancellationToken cancellationToken = default) => SendMessage<RequestSessionData, SessionData>(new RequestSessionData(), cancellationToken);

        public Task<SlotData> GetSlotData(GetSlot request, CancellationToken cancellationToken = default) => SendMessage<GetSlot, SlotData>(request, cancellationToken);
        public Task<ComponentData> GetComponentData(GetComponent request, CancellationToken cancellationToken = default) => SendMessage<GetComponent, ComponentData>(request, cancellationToken);

        public Task<Response> AddSlot(AddSlot request, CancellationToken cancellationToken = default) => SendMessage<AddSlot, Response>(request, cancellationToken);
        public Task<Response> UpdateSlot(UpdateSlot request, CancellationToken cancellationToken = default) => SendMessage<UpdateSlot, Response>(request, cancellationToken);
        public Task<Response> RemoveSlot(RemoveSlot request, CancellationToken cancellationToken = default) => SendMessage<RemoveSlot, Response>(request, cancellationToken);

        public Task<Response> AddComponent(AddComponent request, CancellationToken cancellationToken = default) => SendMessage<AddComponent, Response>(request, cancellationToken);
        public Task<Response> UpdateComponent(UpdateComponent request, CancellationToken cancellationToken = default) => SendMessage<UpdateComponent, Response>(request, cancellationToken);
        public Task<Response> RemoveComponent(RemoveComponent request, CancellationToken cancellationToken = default) => SendMessage<RemoveComponent, Response>(request, cancellationToken);

        public Task<AssetData> ImportTexture(ImportTexture2DFile request, CancellationToken cancellationToken = default) => SendMessage<ImportTexture2DFile, AssetData>(request, cancellationToken);
        public Task<AssetData> ImportTexture(ImportTexture2DRawData request, CancellationToken cancellationToken = default) => SendMessage<ImportTexture2DRawData, AssetData>(request, cancellationToken);

        public Task<AssetData> ImportMesh(ImportMeshJSON request, CancellationToken cancellationToken = default) => SendMessage<ImportMeshJSON, AssetData>(request, cancellationToken);
        public Task<AssetData> ImportMesh(ImportMeshRawData request, CancellationToken cancellationToken = default) => SendMessage<ImportMeshRawData, AssetData>(request, cancellationToken);

        public Task<AssetData> ImportAudioClip(ImportAudioClipFile request, CancellationToken cancellationToken = default) => SendMessage<ImportAudioClipFile, AssetData>(request, cancellationToken);
        public Task<AssetData> ImportAudioClip(ImportAudioClipRawData request, CancellationToken cancellationToken = default) => SendMessage<ImportAudioClipRawData, AssetData>(request, cancellationToken);

        #endregion

        public void Dispose()
        {
            _receiverCts.Cancel();
            _receiverCts.Dispose();
            _clientWriteLock.Dispose();
            _client.Dispose();
            foreach (var children in _pendingResponses.Values)
            {
                children.TrySetCanceled();
            }
        }
    }
}
