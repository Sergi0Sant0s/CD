using System.Threading.Tasks;

namespace Api.Model.IServices.Users
{
    public interface ILoginService
    {
        Task<object> LoginUser(string username, string password);

        Task<bool> UserExists(string username);
    }
}
