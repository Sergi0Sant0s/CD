using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Model.Entities;
using Api.Model.IServices.Chat;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
    public class ChatRepository : IChatRepository
    {
        protected readonly MyContext _context;
        private DbSet<ChatEntity> _datasetChat; //Para não ter de colocar o context.Set...
        private DbSet<MessageEntity> _datasetMessage;

        public ChatRepository(MyContext context)
        {
            _context = context;
            _datasetChat = context.Set<ChatEntity>();
            _datasetMessage = context.Set<MessageEntity>();

        }

        public async Task<bool> DeleteChatAsync(int idChat)
        {
            try
            {
                var checkChat = await _context.Chats.SingleOrDefaultAsync(p => p.Id == idChat);
                if (checkChat != null)
                    return false;

                //Remove messages
                var messages = await _context.Messages.Where(p => p.IdChat == idChat).ToListAsync();
                foreach (var aux in messages)
                    _context.Messages.Remove(aux);

                //Remove chat
                _context.Chats.Remove((ChatEntity)checkChat);

                _context.SaveChanges();

                return true;
            }
            catch (System.Exception)
            {

                return false;
            }

        }

        public async Task<IEnumerable<ChatEntity>> GetAllChatsAsync()
        {
            return await _context.Chats.ToListAsync();
        }

        public async Task<ChatEntity> GetByIdAsync(int id)
        {
            return await _datasetChat.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ChatEntity> NewChatAsync(string name, string description)
        {
            try
            {
                var checkChat = await _datasetChat.FirstOrDefaultAsync(p => p.Name.ToUpper() == name.ToUpper());
                if (checkChat == null)
                {
                    var newChat = new ChatEntity()
                    {
                        Name = name,
                        Description = description
                    };
                    await _datasetChat.AddAsync(newChat);
                    _context.SaveChanges();

                    return newChat;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ChatEntity> UpdateNameAsync(int idChat, string name, string description)
        {
            try
            {
                var checkChat = await _datasetChat.FirstOrDefaultAsync(p => p.Name.ToUpper() == name.ToUpper());
                if (checkChat == null)
                    return null;

                checkChat.Name = name;
                checkChat.Description = description;
                _context.SaveChanges();

                return checkChat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<MessageEntity>> GetMessagesByChatAsync(int idChat)
        {
            //throw new NotImplementedException();
            return await _context.Messages.Where(p => p.IdChat == idChat).ToListAsync();
        }

        public async Task<MessageEntity> NewMessageAsync(int idChat, int idUser, string text, DateTime time)
        {
            try
            {
                var message = await _datasetMessage.SingleOrDefaultAsync(p => p.IdChat == idChat
                                                              && p.IdUser == idUser
                                                              && p.Text == text
                                                              && p.Time == time);
                if (message != null)
                    return null;

                var newMessage = new MessageEntity()
                {
                    IdChat = idChat,
                    IdUser = idUser,
                    Text = text,
                    Time = time
                };
                _context.Messages.Add(newMessage);
                _context.SaveChanges();
                return newMessage;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<object>> GetAllMessagesAsync()
        {
            try
            {
                var chats = await GetAllChatsAsync();
                List<object> objects = new List<object>();


                foreach (var item in chats)
                {
                    var messages = await GetMessagesByChatAsync(item.Id);
                    List<object> objs = new List<object>();

                    foreach (var message in messages)
                    {
                        objs.Add(new
                        {
                            chat = item.Id,
                            username = _context.Users.FirstOrDefault(p => p.Id == message.IdUser).Username,
                            name = _context.Users.FirstOrDefault(p => p.Id == message.IdUser).Name,
                            message = message.Text,
                            date = message.Time
                        });
                    }

                    var chatObj = new
                    {
                        id = item.Id,
                        name = item.Name,
                        description = item.Description,
                        messages = objs
                    };
                    objects.Add(chatObj);
                }

                return objects;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
