using System.Threading.Tasks;
using Api.Model.Entities;

namespace Api.Model.IServices.Users
{
    public interface IUserRepository
    {

        Task<UserEntity> NewUserAsync(string name, string username, string password, string email, string imagePath, string folderPath);

        Task<UserEntity> UpdateUserAsync(string name, string username, string email, string imagePath, string folderPath);

        Task<bool> RemoveUserAsync(int Id);
    }
}
