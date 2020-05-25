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

    /// <summary>
    /// Apagar um chat
    /// </summary>
    /// <param name="idChat">Id do chat</param>
    /// <returns>Retorna se foi possivel apagar o chat</returns>
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

    /// <summary>
    /// Criar um novo chat
    /// </summary>
    /// <param name="name">Nome do chat</param>
    /// <param name="description">Descrição do chat</param>
    /// <returns></returns>
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

    /// <summary>
    /// Alterar o nome de um chat
    /// </summary>
    /// <param name="idChat">Id do chat para alterar o seu nome</param>
    /// <param name="name">Novo nome para o chat</param>
    /// <param name="description">Descrição do chat</param>
    /// <returns></returns>
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

    /// <summary>
    /// Obter as mensagens de um chat
    /// </summary>
    /// <param name="idChat">Id do chat</param>
    /// <returns></returns>
    public async Task<IEnumerable<MessageEntity>> GetMessagesByChatAsync(int idChat)
    {
      //throw new NotImplementedException();
      return await _context.Messages.Where(p => p.IdChat == idChat).ToListAsync();
    }

    /// <summary>
    /// Nova mensagem
    /// </summary>
    /// <param name="idChat">Id do chat para onde a mensagem vai ser enviada</param>
    /// <param name="user">User que envia a mensagem</param>
    /// <param name="text">Conteudo da mensagem</param>
    /// <param name="time">Data de envio da mensagem</param>
    /// <returns></returns>
    public async Task<MessageEntity> NewMessageAsync(int idChat, string user, string text, DateTime time)
    {
      try
      {
        var userCheck = await _context.Users.FirstOrDefaultAsync(p => p.Username.ToUpper() == user);
        if (userCheck == null)
          return null;

        var message = await _datasetMessage.SingleOrDefaultAsync(p => p.IdChat == idChat
                                                      && p.IdUser == userCheck.Id
                                                      && p.Text == text
                                                      && p.Time == time);
        if (message != null)
          return null;

        var newMessage = new MessageEntity()
        {
          IdChat = idChat,
          IdUser = userCheck.Id,
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

    /// <summary>
    /// Obter todas as mensagens
    /// </summary>
    /// <returns>Retorna uma lista com as mensagens todas</returns>
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
            var user = _context.Users.FirstOrDefault(p => p.Id == message.IdUser);
            objs.Add(new
            {
              chat = item.Id,
              username = user.Username,
              name = user.Name,
              message = message.Text,
              date = message.Time.Value.ToString("dd/MM/yyyy HH:mm")
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
