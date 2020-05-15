using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Sockets
{
    public abstract class WebSocketHandler
    {
        protected ConnectionManager WebSocketConnectionManager { get; set; }

        public WebSocketHandler(ConnectionManager webSocketConnectionManager)
        {
            WebSocketConnectionManager = webSocketConnectionManager;
        }

        public virtual async Task OnConnected(string username, WebSocket socket, Dictionary<string, string> values)
        {
            WebSocketConnectionManager.AddSocket(username, socket);
        }

        public virtual async Task OnDisconnected(WebSocket socket)
        {
            await WebSocketConnectionManager.RemoveSocket(WebSocketConnectionManager.GetId(socket));
        }

        public abstract Task SendMessageAsync(WebSocket socket, string message);

        public abstract Task SendMessageToAllAsync(string json);

        public abstract Task ReceiveAsync(string username, WebSocket socket, WebSocketReceiveResult result, Dictionary<string, string> values);

        //public abstract bool CheckIfExistsConnection(string username);
    }
}
