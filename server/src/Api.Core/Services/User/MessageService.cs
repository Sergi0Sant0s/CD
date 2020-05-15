using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Model.Entities;
using Api.Model.IServices.Chat;

namespace Api.Core.Services.User
{
    public class MessageService : IMessageService
    {
        IMessageRepository _message;
        public MessageService(IMessageRepository message)
        {
            _message = message;
        }

        public async Task<IEnumerable<MessageEntity>> GetMessagesbyChat(int idChat)
        {
            return await _message.GetMessagesByChatAsync(idChat);
        }

        public async Task<MessageEntity> NewMessage(int idChat, int idUser, string text, DateTime time)
        {
            return await _message.NewMessageAsync(idChat, idUser, text, time);
        }
    }
}
