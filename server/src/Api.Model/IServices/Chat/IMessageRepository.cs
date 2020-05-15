using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Model.Entities;

namespace Api.Model.IServices.Chat
{
    public interface IMessageRepository
    {
        Task<MessageEntity> NewMessageAsync(int idChat, int idUser, string text, DateTime time);

        Task<IEnumerable<MessageEntity>> GetMessagesByChatAsync(int idChat);

    }
}
