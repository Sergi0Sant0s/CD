

using Api.Model.Security;

namespace Api.Core.Token
{
  public class TokenMng
  {
    /// <summary>
    /// Validar um token
    /// </summary>
    /// <param name="token">Token a ser validado</param>
    /// <param name="info">Informaçao sobre o token</param>
    /// <returns>Retorna se o token é valido ou não</returns>
    public static bool ValidateToken(string token, out object info)
    {
      var aux = token.Replace("Bearer ", "");
      return TokenManager.ValidateToken(aux, out info);
    }

    /// <summary>
    /// Remover um token
    /// </summary>
    /// <param name="token">Token a remover</param>
    /// <returns>Retorna se o token foi removido ou não</returns>
    public static bool RemoveToken(string token)
    {
      var aux = token.Replace("Bearer ", "");
      return TokenManager.RemoveToken(aux);
    }

    /// <summary>
    /// Obter o username do user que está ligado com o token
    /// </summary>
    /// <param name="token">Token que queremos descobrir o user que o está a usar</param>
    /// <returns>Retorna o nome do user que está a usar o token</returns>
    public static string UsernameToken(string token)
    {
      return TokenManager.GetUserByToken(token.Replace("Bearer ", ""));
    }
  }
}
