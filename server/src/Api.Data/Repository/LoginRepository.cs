using System;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Model.Entities;
using Api.Model.IServices.Users;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
  public class LoginRepository : ILoginRepository
  {
    private DbSet<UserEntity> _dataset;

    public LoginRepository(MyContext context)
    {
      _dataset = context.Set<UserEntity>();
    }

    /// <summary>
    /// Login
    /// </summary>
    /// <param name="username">Username</param>
    /// <param name="password">Password</param>
    /// <returns></returns>
    public async Task<UserEntity> LoginUserAsync(string username, string password)
    {
      return await _dataset.FirstOrDefaultAsync(p => p.Username.ToUpper().Equals(username.ToUpper())
                                                  && p.Password.Equals(password));
    }

    /// <summary>
    /// Verifica se o user já está logado
    /// </summary>
    /// <param name="username">Username</param>
    /// <returns></returns>
    public async Task<bool> UserExistsAsync(string username)
    {
      return await _dataset.FirstOrDefaultAsync(p => p.Username.ToUpper().Equals(username.ToUpper())) == null ? false : true;
    }
  }
}
