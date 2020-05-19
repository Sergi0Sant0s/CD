using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Application.Sockets
{
    public class ConnectionManager
    {
        private ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

        public WebSocket GetSocketById(string id)
        {
            return _sockets.FirstOrDefault(p => p.Key == id).Value;
        }

        public ConcurrentDictionary<string, WebSocket> GetAll()
        {
            return _sockets;
        }

        public string GetId(WebSocket socket)
        {
            return _sockets.FirstOrDefault(p => p.Value == socket).Key;
        }
        public async void AddSocket(string username, WebSocket socket)
        {
            var aux = _sockets.FirstOrDefault(p => p.Key == username);
            if (aux.Value == null)
                _sockets.TryAdd(username, socket);
            else
            {
                try
                {
                    await RemoveSocket(aux.Key);
                    _sockets.TryAdd(username, socket);
                }
                catch (System.Exception ex)
                {
                    if (aux.Value.State == WebSocketState.Open)
                    {
                        await socket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure,
                                        statusDescription: "Fechado pelo gestor de sockets",
                                        cancellationToken: CancellationToken.None);
                    }
                    WebSocket tempSocket;
                    _sockets.TryRemove(aux.Key, out tempSocket);
                }
            }
        }

        public async Task RemoveSocket(string id)
        {
            WebSocket socket;
            _sockets.TryRemove(id, out socket);
        }

        public bool CheckIfExistsConnection(string username)
        {
            return _sockets.ContainsKey(username);
        }
    }
}
