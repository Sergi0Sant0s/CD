using System.Threading.Tasks;
using Api.Model.Entities;

namespace Api.Model.IServices.Users
{
    public interface IUserService
    {

        Task<UserEntity> NewUser(string name, string username, string password, string email, string imagePath, string folderPath);

        Task<UserEntity> UpdateUser(string name, string username, string email, string imagePath, string folderPath);

        Task<bool> RemoveUser(int Id);
    }
}
