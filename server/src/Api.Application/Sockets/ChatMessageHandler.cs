using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Api.Core.Services.User;
using Api.Core.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Application.Sockets
{
    public class ChatMessageHandler : WebSocketHandler
    {
        public ChatMessageHandler(ConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager) { }

        public override async Task OnConnected(string username, WebSocket socket, Dictionary<string, string> values)
        {

            await base.OnConnected(username, socket, values);
            //
            string json = "{ \"type\": \"200\" , \"message\" : \"" + username + " juntou-se a conversa\"}";
            await SendMessageToAllAsync(json);
        }

        public override async Task ReceiveAsync(string username, WebSocket socket, WebSocketReceiveResult result, Dictionary<string, string> values)
        {
            if (!values.ContainsKey("chat") || !values.ContainsKey("message") || !values.ContainsKey("time"))
                await WebSocketConnectionManager.RemoveSocket(WebSocketConnectionManager.GetId(socket));
            else
            {
                DateTime time = DateTime.Parse(values["time"]);
                string json = jsonBuilder(username, values["chat"], values["message"], time);
                //
                await SendMessageToAllAsync(json.ToString());
            }
        }

        public override async Task SendMessageAsync(WebSocket socket, string message)
        {
            if (socket.State != WebSocketState.Open)
                return;

            await socket.SendAsync(buffer: new ArraySegment<byte>(array: Encoding.ASCII.GetBytes(message),
                                                                        offset: 0,
                                                                        count: message.Length),
                                        messageType: WebSocketMessageType.Text,
                                        endOfMessage: true,
                                        cancellationToken: CancellationToken.None);
        }

        public override async Task SendMessageToAllAsync(string json)
        {
            foreach (var pair in WebSocketConnectionManager.GetAll())
            {
                if (pair.Value.State == WebSocketState.Open)
                    await SendMessageAsync(pair.Value, json);
            }
        }


        public string jsonBuilder(string name, string chat, string message, DateTime time)
        {
            StringBuilder json = new StringBuilder();
            json.Append("{");
            json.Append("\"type\":");
            json.Append("\"" + 200 + "\",");
            json.Append("\"name\":");
            json.Append("\"" + name + "\",");
            json.Append("\"chat\":");
            json.Append("\"" + chat + "\",");
            json.Append("\"message\":");
            json.Append("\"" + message + "\",");
            json.Append("\"time\":");
            json.Append("\"" + time + "\"");
            json.Append("}");

            return json.ToString();
        }

        /*public override bool CheckIfExistsConnection(string username)
        {
            throw new NotImplementedException();
        }*/
    }
}
