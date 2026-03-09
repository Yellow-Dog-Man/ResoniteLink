using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ResoniteLink
{
    public class LinkSessionListener : IDisposable
    {
        public const int ANNOUNCE_PORT = 12512;
        public static readonly TimeSpan ANNOUNCE_INTERVAL = TimeSpan.FromSeconds(10);

        public event Action<ResoniteLinkSession> SessionDiscovered;
        public event Action<ResoniteLinkSession> SessionUpdated;
        public event Action<ResoniteLinkSession> SessionClosed;

        public void GetDiscoveredSessions(List<ResoniteLinkSession> sessions)
        {
            lock (_sessions)
                sessions.AddRange(_sessions.Values);
        }

        Dictionary<string, ResoniteLinkSession> _sessions = new Dictionary<string, ResoniteLinkSession>();

        UdpClient _listener;

        CancellationTokenSource cancellationTokenSource;

        public LinkSessionListener()
        {
            cancellationTokenSource = new CancellationTokenSource();
        }

        public void Start()
        {
            if (_listener != null)
                throw new InvalidOperationException("Listener is already started");

            _listener = new UdpClient();
            _listener.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            _listener.Client.Bind(new IPEndPoint(IPAddress.Any, ANNOUNCE_PORT));

            Task.Run(async () => await ReceiveAnnoucements(cancellationTokenSource.Token).ConfigureAwait(false));
            Task.Run(async () => await ProcessCleanups(cancellationTokenSource.Token).ConfigureAwait(false));
        }

        async Task ProcessCleanups(CancellationToken cancellation)
        {
            while (!cancellation.IsCancellationRequested)
            {
                await Task.Delay(ANNOUNCE_INTERVAL);
                ExpireSessions();
            }
        }

        async Task ReceiveAnnoucements(CancellationToken cancellation)
        {
            var endpoint = new IPEndPoint(IPAddress.Any, 0);

            while (!cancellation.IsCancellationRequested)
            {
                try
                {
                    var data = await _listener.ReceiveAsync().ConfigureAwait(false);

                    if (data.Buffer != null)
                        Decode(data.RemoteEndPoint, data.Buffer);
                }
                catch (OperationCanceledException)
                {
                    // ignore, this is fine, just means that the receiving was canceled
                }
                catch (Exception ex)
                {
                    // Swallow the exceptions
                    // Normally this isn't best practice, but this is simple enough mechanism where little can legitimately break
                    // Most cases it will be some networking error or possibly parsing error for the JSON or the data
                    // We want to ignore those and keep receiving data, because it's better than the discovery just stopping to work
                    // due to some unexpected error
                }
            }
        }

        void ExpireSessions()
        { 
            lock(_sessions)
            {
                List<string> _expiredKeys = null;

                foreach(var session in _sessions)
                    if(IsExpired(session.Value))
                    {
                        SessionClosed?.Invoke(session.Value);

                        if (_expiredKeys == null)
                            _expiredKeys = new List<string>();

                        _expiredKeys.Add(session.Key);
                    }

                if (_expiredKeys != null)
                    foreach (var key in _expiredKeys)
                        _sessions.Remove(key);
            }
        }

        // Consider them expired if they failed to send in 2.5x of the normal annouce interval
        bool IsExpired(ResoniteLinkSession session) => (DateTime.UtcNow - session.LastUpdateTimestamp).TotalSeconds > ANNOUNCE_INTERVAL.TotalSeconds * 2.5f;

        void Decode(IPEndPoint endpoint, byte[] data)
        {
            using (var stream = new MemoryStream(data))
            {
                var sessionInfo = System.Text.Json.JsonSerializer.Deserialize<ResoniteLinkSession>(stream);

                // Ignore if the SessionId is not set. This is not a valid session.
                if (string.IsNullOrEmpty(sessionInfo.SessionId))
                    return;

                lock(_sessions)
                {
                    sessionInfo.LastUpdateTimestamp = DateTime.UtcNow;
                    sessionInfo.LinkEndPoint = endpoint;

                    if (sessionInfo.LinkPort < 0)
                    {
                        // This indicates that the session is closed, remove it
                        if(_sessions.Remove(sessionInfo.SessionId))
                            SessionClosed?.Invoke(sessionInfo);

                        return;
                    }

                    if(_sessions.TryGetValue(sessionInfo.SessionId, out var existingInfo))
                    {
                        // Update the session info
                        _sessions[sessionInfo.SessionId] = sessionInfo;
                        SessionUpdated?.Invoke(sessionInfo);
                    }
                    else
                    {
                        _sessions.Add(sessionInfo.SessionId, sessionInfo);
                        SessionDiscovered?.Invoke(sessionInfo);
                    }
                }
            }
        }

        public void Dispose()
        {
            cancellationTokenSource.Cancel();
            _listener.Dispose();
        }
    }
}
