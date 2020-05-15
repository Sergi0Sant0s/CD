using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Model.Entities;

namespace Api.Model.IServices.Chat
{
    public interface IChatRepository
    {
        Task<ChatEntity> NewChatAsync(string name);

        Task<ChatEntity> UpdateNameAsync(int idChat, string name);

        Task<ChatEntity> GetByIdAsync(int id);

        Task<bool> DeleteChatAsync(int idChat);

        Task<IEnumerable<ChatEntity>> GetAllChats();

    }
}
