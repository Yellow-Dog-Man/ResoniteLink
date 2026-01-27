using System;
using System.Collections.Generic;
using System.Text;
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

        ClientWebSocket _client;
        CancellationTokenSource cancellation;

        JsonSerializerOptions _options;

        ConcurrentDictionary<string, TaskCompletionSource<Response>> _pendingResponses = new ConcurrentDictionary<string, TaskCompletionSource<Response>>();

        public LinkInterface()
        {
            _options = new JsonSerializerOptions()
            {
                // Necessary for values like Infinity, NaN and so on
                NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals,
                AllowOutOfOrderMetadataProperties = true,
            };
        }

        public async Task Connect(Uri target, System.Threading.CancellationToken cancellationToken)
        {
            if(_client != null)
                throw new InvalidOperationException("Client has already been initialized.");

            _client = new ClientWebSocket();

            cancellation = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            await _client.ConnectAsync(target, cancellation.Token);

            _ = Task.Run(async () => ReceiverHandler(cancellation.Token));
        }

        async Task ReceiverHandler(CancellationToken cancellationToken)
        {
            try
            {
                byte[] buffer = new byte[DEFAULT_BUFFER_SIZE];

                int receivedBytes = 0;

                while (!cancellationToken.IsCancellationRequested)
                {
                    if(receivedBytes == buffer.Length)
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
                            cancellation.Cancel();
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
                    FailureException == null ? "Closing" : "Internal Error", cancellation.Token);

            _client.Dispose();
        }

        async Task<O> SendMessage<I,O>(I message)
            where I : Message
            where O : Response
        {
            EnsureMessageID(message);

            var responseCompletion = new TaskCompletionSource<Response>();

            if (!_pendingResponses.TryAdd(message.MessageID, responseCompletion))
                throw new InvalidOperationException("Failed to register MessageID. Did you provide duplicate MessageID?");

            var jsonData = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes((Message)message);

            await _client.SendAsync(new ArraySegment<byte>(jsonData), 
                WebSocketMessageType.Text, true, cancellation.Token);

            if(message is BinaryPayloadMessage binaryPayload)
            {
                // We must send the binary payload as well following the message
                await _client.SendAsync(new ArraySegment<byte>(binaryPayload.RawBinaryPayload), WebSocketMessageType.Binary, true,
                    cancellation.Token);
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

        public Task<SessionData> GetSessionData() => SendMessage<RequestSessionData, SessionData>(new RequestSessionData());

        public Task<SlotData> GetSlotData(GetSlot request) => SendMessage<GetSlot, SlotData>(request);
        public Task<ComponentData> GetComponentData(GetComponent request) => SendMessage<GetComponent, ComponentData>(request);

        public Task<Response> AddSlot(AddSlot request) => SendMessage<AddSlot, Response>(request);
        public Task<Response> UpdateSlot(UpdateSlot request) => SendMessage<UpdateSlot, Response>(request);
        public Task<Response> RemoveSlot(RemoveSlot request) => SendMessage<RemoveSlot, Response>(request);

        public Task<Response> AddComponent(AddComponent request) => SendMessage<AddComponent, Response>(request);
        public Task<Response> UpdateComponent(UpdateComponent request) => SendMessage<UpdateComponent, Response>(request);
        public Task<Response> RemoveComponent(RemoveComponent request) => SendMessage<RemoveComponent, Response>(request);

        public Task<AssetData> ImportTexture(ImportTexture2DFile request) => SendMessage<ImportTexture2DFile, AssetData>(request);
        public Task<AssetData> ImportTexture(ImportTexture2DRawData request) => SendMessage<ImportTexture2DRawData, AssetData>(request);

        public Task<AssetData> ImportMesh(ImportMeshJSON request) => SendMessage<ImportMeshJSON, AssetData>(request);
        public Task<AssetData> ImportMesh(ImportMeshRawData request) => SendMessage<ImportMeshRawData, AssetData>(request);

        public Task<AssetData> ImportAudioClip(ImportAudioClipFile request) => SendMessage<ImportAudioClipFile, AssetData>(request);
        public Task<AssetData> ImportAudioClip(ImportAudioClipRawData request) => SendMessage<ImportAudioClipRawData, AssetData>(request);

        public Task<ComponentTypeList> GetComponentTypes(string categoryPath) => SendMessage<GetComponentTypeList, ComponentTypeList>(
            new GetComponentTypeList() { CategoryPath = categoryPath });
        public Task<ComponentTypeList> GetAllComponentTypes() => SendMessage<GetComponentTypeList, ComponentTypeList>(
            new GetComponentTypeList() { CategoryPath = ResoniteLink.GetComponentTypeList.ALL_COMPONENTS });
        public Task<ComponentTypeList> GetComponentTypeList(GetComponentTypeList request) => SendMessage<GetComponentTypeList, ComponentTypeList>(request);

        public Task<ComponentDefinitionData> GetComponentDefinition(GetComponentDefinition request) =>
            SendMessage<GetComponentDefinition, ComponentDefinitionData>(request);

        public Task<ComponentDefinitionData> GetComponentDefinition(string type, bool flattened) => SendMessage<GetComponentDefinition, ComponentDefinitionData>(
            new GetComponentDefinition() { ComponentType = type, Flattened = flattened });
        public Task<TypeDefinitionData> GetTypeDefinition(GetTypeDefinition request) => SendMessage<GetTypeDefinition, TypeDefinitionData>(request);
        public Task<TypeDefinitionData> GetTypeDefinition(string typename) => 
            SendMessage<GetTypeDefinition, TypeDefinitionData>(new GetTypeDefinition() { Type = typename });
        public Task<TypeDefinitionData> GetGenericTypeDefinition(GetGenericTypeDefinition request) => SendMessage<GetGenericTypeDefinition, TypeDefinitionData>(request);
        public Task<TypeDefinitionData> GetGenericTypeDefinition(string typename) =>
            SendMessage<GetGenericTypeDefinition, TypeDefinitionData>(new GetGenericTypeDefinition() { GenericInstanceType = typename });

        public Task<EnumDefinitionData> GetEnumDefinition(GetEnumDefinition request) => SendMessage<GetEnumDefinition, EnumDefinitionData>(request);
        public Task<EnumDefinitionData> GetEnumDefinition(string typename) =>
            SendMessage<GetEnumDefinition, EnumDefinitionData>(new GetEnumDefinition() { Type = typename });

        #endregion

        public void Dispose()
        {
            cancellation.Cancel();

            // Cancel all the pending responses
            foreach (var pending in _pendingResponses)
                pending.Value.TrySetCanceled();
        }
    }
}
