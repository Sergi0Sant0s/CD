using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Model.Entities;

namespace Api.Model.IServices.Chat
{
    public interface IChatService
    {
        Task<ChatEntity> NewChat(string name, string description);

        Task<ChatEntity> UpdateName(int idChat, string name, string description);

        Task<bool> DeleteChat(int idChat);

        Task<ChatEntity> GetById(int id);

        Task<IEnumerable<ChatEntity>> GetAllChats();

        Task<IEnumerable<object>> GetAllMessages();

        Task<MessageEntity> NewMessage(int idChat, int idUser, string text, DateTime time);

        Task<IEnumerable<MessageEntity>> GetMessagesbyChat(int idChat);
    }
}
