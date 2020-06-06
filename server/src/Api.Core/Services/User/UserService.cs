using System.Threading.Tasks;
using Api.Model.Entities;
using Api.Model.IServices.Users;

namespace Api.Core.Services.User
{
    public class UserService : IUserService
    {
        private IUserRepository _user;
        public UserService(IUserRepository user)
        {
            _user = user;
        }

        public async Task<UserEntity> NewUser(string name, string username, string password, string email, string imagePath)
        {
            return await _user.NewUserAsync(name, username, password, email, imagePath);
        }

        public async Task<UserEntity> UpdateUser(string name, string username, string email, string imagePath)
        {
            return await _user.UpdateUserAsync(name, username, email, imagePath);
        }

        public async Task<bool> RemoveUser(int id)
        {
            return await _user.RemoveUserAsync(id);
        }

        public Task<UserEntity> GetUserById(int id)
        {
            return _user.GetUserByIdAsync(id);
        }
    }
}
