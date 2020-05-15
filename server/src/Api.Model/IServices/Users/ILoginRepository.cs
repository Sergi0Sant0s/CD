using System.Threading.Tasks;
using Api.Model.Entities;

namespace Api.Model.IServices.Users
{
    public interface ILoginRepository
    {
        Task<UserEntity> LoginUserAsync(string username, string password);

        Task<bool> UserExistsAsync(string username);
    }
}
