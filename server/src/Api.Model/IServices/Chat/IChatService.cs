using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Model.Entities;

namespace Api.Model.IServices.Chat
{
    public interface IChatService
    {
        Task<ChatEntity> NewChat(string name);

        Task<ChatEntity> UpdateName(int idChat, string name);

        Task<bool> DeleteChat(int idChat);

        Task<ChatEntity> GetById(int id);

        Task<IEnumerable<ChatEntity>> GetAllChats();
    }
}
