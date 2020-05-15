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
        private DbSet<ChatEntity> _dataset; //Para não ter de colocar o context.Set...

        public ChatRepository(MyContext context)
        {
            _context = context;
            _dataset = context.Set<ChatEntity>();
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

        public async Task<IEnumerable<ChatEntity>> GetAllChats()
        {
            return await _context.Chats.ToListAsync();
        }

        public async Task<ChatEntity> GetByIdAsync(int id)
        {
            return await _dataset.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ChatEntity> NewChatAsync(string name)
        {
            try
            {
                var checkChat = await _dataset.FirstOrDefaultAsync(p => p.Name.ToUpper() == name.ToUpper());
                if (checkChat == null)
                {
                    var newChat = new ChatEntity()
                    {
                        Name = name
                    };
                    await _dataset.AddAsync(newChat);
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

        public async Task<ChatEntity> UpdateNameAsync(int idChat, string name)
        {
            try
            {
                var checkChat = await _dataset.FirstOrDefaultAsync(p => p.Name.ToUpper() == name.ToUpper());
                if (checkChat == null)
                    return null;

                checkChat.Name = name;
                _context.SaveChanges();

                return checkChat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
