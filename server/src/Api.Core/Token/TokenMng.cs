

using Api.Model.Security;

namespace Api.Core.Token
{
    public class TokenMng
    {
        public static bool ValidateToken(string token)
        {
            var aux = token.Replace("Bearer ", "");
            return TokenManager.ValidateToken(aux);
        }

        public static bool RemoveToken(string token)
        {
            var aux = token.Replace("Bearer ", "");
            return TokenManager.RemoveToken(aux);
        }

        public static string UsernameToken(string token)
        {
            return TokenManager.GetUserByToken(token.Replace("Bearer ", ""));
        }
    }
}
