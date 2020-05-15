using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Model.Entities;
using Api.Model.IServices.Chat;

namespace Api.Core.Services.User
{
    public class ChatService : IChatService
    {
        private IChatRepository _chat;
        public ChatService(IChatRepository chat)
        {
            _chat = chat;
        }

        public async Task<bool> DeleteChat(int idChat)
        {
            return await _chat.DeleteChatAsync(idChat);
        }

        public async Task<IEnumerable<ChatEntity>> GetAllChats()
        {
            return await _chat.GetAllChats();
        }

        public async Task<ChatEntity> GetById(int id)
        {
            return await _chat.GetByIdAsync(id);
        }

        public async Task<ChatEntity> NewChat(string name)
        {
            return await _chat.NewChatAsync(name);
        }

        public async Task<ChatEntity> UpdateName(int idChat, string name)
        {
            return await _chat.UpdateNameAsync(idChat, name);
        }
    }
}
