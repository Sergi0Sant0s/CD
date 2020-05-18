using System;
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
            return await _chat.GetAllChatsAsync();
        }

        public async Task<ChatEntity> GetById(int id)
        {
            return await _chat.GetByIdAsync(id);
        }

        public async Task<ChatEntity> NewChat(string name, string description)
        {
            return await _chat.NewChatAsync(name, description);
        }

        public async Task<ChatEntity> UpdateName(int idChat, string name, string description)
        {
            return await _chat.UpdateNameAsync(idChat, name, description);
        }

        public async Task<IEnumerable<MessageEntity>> GetMessagesbyChat(int idChat)
        {
            return await _chat.GetMessagesByChatAsync(idChat);
        }

        public async Task<MessageEntity> NewMessage(int idChat, int idUser, string text, DateTime time)
        {
            return await _chat.NewMessageAsync(idChat, idUser, text, time);
        }

        public async Task<IEnumerable<object>> GetAllMessages()
        {
            return await _chat.GetAllMessagesAsync();
        }
    }
}
