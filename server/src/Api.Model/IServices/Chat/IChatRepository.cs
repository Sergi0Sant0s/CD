using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Model.Entities;

namespace Api.Model.IServices.Chat
{
    public interface IChatRepository
    {
        Task<ChatEntity> NewChatAsync(string name, string description);

        Task<ChatEntity> UpdateNameAsync(int idChat, string name, string description);

        Task<ChatEntity> GetByIdAsync(int id);

        Task<bool> DeleteChatAsync(int idChat);

        Task<IEnumerable<ChatEntity>> GetAllChatsAsync();

        Task<IEnumerable<object>> GetAllMessagesAsync();

        Task<MessageEntity> NewMessageAsync(int idChat, int idUser, string text, DateTime time);

        Task<IEnumerable<MessageEntity>> GetMessagesByChatAsync(int idChat);

    }
}
