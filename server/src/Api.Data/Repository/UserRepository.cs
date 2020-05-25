using System;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Model;
using Api.Model.Entities;
using Api.Model.IServices.Users;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
  public class UserRepository : IUserRepository
  {

    protected readonly MyContext _context;
    private DbSet<UserEntity> _dataset; //Para não ter de colocar o context.Set...

    public UserRepository(MyContext context)
    {
      _context = context;
      _dataset = context.Set<UserEntity>();
    }

    /// <summary>
    /// Novo user
    /// </summary>
    /// <param name="name">Nome</param>
    /// <param name="username">Username</param>
    /// <param name="password">Password</param>
    /// <param name="email">Email</param>
    /// <param name="imagePath">Caminho/Diretorio para as imagens</param>
    /// <param name="folderPath">Caminho/Diretorio para as pastas</param>
    /// <returns></returns>
    public async Task<UserEntity> NewUserAsync(string name, string username, string password, string email, string imagePath, string folderPath)
    {
      try
      {
        var user = await _dataset.SingleOrDefaultAsync(a => a.Username.ToUpper().Equals(username.ToUpper()));
        if (user != null)
          return null;

        var newUser = new UserEntity()
        {
          Name = name,
          Username = username,
          Password = password,
          Email = email,
          ImagePath = imagePath,
          FolderPath = folderPath
        };
        _context.Users.Add(newUser);
        _context.SaveChanges();
        return newUser;
      }
      catch (Exception ex)
      {

        throw ex;
      }
    }

    /// <summary>
    /// Update aos dados do user
    /// </summary>
    /// <param name="name">Nome</param>
    /// <param name="username">Username</param>
    /// <param name="email">Email</param>
    /// <param name="imagePath">Caminho/Diretorio para as imagens</param>
    /// <param name="folderPath">Caminho/Diretorio para as pastas</param>
    /// <returns></returns>
    public async Task<UserEntity> UpdateUserAsync(string name, string username, string email, string imagePath, string folderPath)
    {
      try
      {
        var user = await _dataset.SingleOrDefaultAsync(a => a.Username.ToUpper().Equals(username.ToUpper()));
        if (user == null) return null;

        user.Name = name;
        user.Email = email;
        user.ImagePath = imagePath;
        user.FolderPath = folderPath;
        _context.SaveChanges();
        return user;
      }
      catch (Exception ex)
      {

        throw ex;
      }
    }


    /// <summary>
    /// Remover um user
    /// </summary>
    /// <param name="id">Id do user a apagar</param>
    /// <returns></returns>
    public async Task<bool> RemoveUserAsync(int id)
    {
      try
      {
        var user = await _dataset.SingleOrDefaultAsync(a => a.Id.Equals(id));
        if (user == null) return false;

        _context.Remove(user);
        _context.SaveChanges();
        return true;
      }
      catch (Exception ex)
      {

        throw ex;
      }
    }

    /// <summary>
    /// Procurar um user pelo seu id
    /// </summary>
    /// <param name="id">Id do user</param>
    /// <returns></returns>
    public Task<UserEntity> GetUserByIdAsync(int id)
    {
      try
      {
        return _dataset.FirstOrDefaultAsync(p => p.Id.Equals(id));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
