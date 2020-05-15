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
    public class MessageRepository : IMessageRepository
    {
        protected readonly MyContext _context;
        private DbSet<MessageEntity> _dataset; //Para não ter de colocar o context.Set...

        public MessageRepository(MyContext context)
        {
            _context = context;
            _dataset = context.Set<MessageEntity>();
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
                var message = await _dataset.SingleOrDefaultAsync(p => p.IdChat == idChat
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
    }
}
