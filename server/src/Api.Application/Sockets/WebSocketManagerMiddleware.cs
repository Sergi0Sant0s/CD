using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Api.Core.Token;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Application.Sockets
{
    public class WebSocketManagerMiddleware
    {
        private readonly RequestDelegate _next;
        private WebSocketHandler _webSocketHandler { get; set; }

        public WebSocketManagerMiddleware(RequestDelegate next,
                                            WebSocketHandler webSocketHandler)
        {
            _next = next;
            _webSocketHandler = webSocketHandler;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
                return;

            var socket = await context.WebSockets.AcceptWebSocketAsync();



            await Receive(socket, async (result, buffer) =>
                        {
                            try
                            {
                                var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(Encoding.UTF8.GetString(buffer, 0, result.Count));
                                if (values != null && values.ContainsKey("token") && TokenMng.ValidateToken(values["token"]))
                                {
                                    var username = TokenMng.UsernameToken(values["token"]);

                                    if (result.MessageType == WebSocketMessageType.Text)
                                    {
                                        if (values.ContainsKey("type") && values["type"] == "1" && values.ContainsKey("token"))
                                        {
                                            await _webSocketHandler.ReceiveAsync(username, socket, result, values);
                                            return;
                                        }
                                        else if (values.ContainsKey("type") && values["type"] == "authenticate")
                                        {
                                            await _webSocketHandler.OnConnected(username, socket, values);
                                            return;
                                        }
                                    }
                                    else if (result.MessageType == WebSocketMessageType.Close)
                                    {
                                        await _webSocketHandler.OnDisconnected(socket);
                                        return;
                                    }
                                }
                                else if (values != null)
                                {

                                }
                                else
                                {
                                    return;
                                }

                            }
                            catch (System.Exception)
                            {
                                throw;
                            }
                        });
        }

        private async Task Receive(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer),
                                                        cancellationToken: CancellationToken.None);

                handleMessage(result, buffer);
            }
        }
    }
}
