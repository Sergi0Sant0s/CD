using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Api.Model.Entities;
using Api.Model.Security;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Api.Model.IServices.Users;

namespace Api.Core.Services.User
{
    public class LoginService : ILoginService
    {
        private ILoginRepository _user;

        private SigningConfigurations _signingConfigurations;

        private TokenConfigurations _tokenConfigurations;

        private IConfiguration _configuration;

        public LoginService(ILoginRepository user,
                            SigningConfigurations signingConfigurations,
                            TokenConfigurations tokenConfigurations,
                            IConfiguration configuration)
        {
            _user = user;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
            _configuration = configuration;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _user.UserExistsAsync(username);
        }

        public async Task<object> LoginUser(string username, string password)
        {
            var baseUser = new UserEntity();

            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                baseUser = await _user.LoginUserAsync(username, password);
                //
                if (baseUser == null)
                {
                    return await _user.UserExistsAsync(username) ? new //utilizador existe
                    {
                        authentication = false,
                        message = "Password invalida"
                    } :
                    new //Utilizador não existe
                    {
                        authentication = false,
                        message = "Utilizador não existe"
                    };

                }
                else
                {
                    var identity = new ClaimsIdentity(
                        new GenericIdentity(baseUser.Username),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //jti 0 id do token
                            new Claim(JwtRegisteredClaimNames.UniqueName, baseUser.Username)
                        }
                    );

                    DateTime createDate = DateTime.Now;
                    DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

                    var handler = new JwtSecurityTokenHandler();
                    string token = CreateToken(identity, createDate, expirationDate, handler);
                    new TokenManager(baseUser.Name, baseUser.Username, expirationDate, token);
                    return SuccessObject(createDate, expirationDate, token, baseUser);

                }
            }
            else
                return new
                {
                    authentication = false,
                    message = string.Format("Dados invalidos:\n\tUtilizador/Password com epaços em branco;\n\tUtilizador/Password não preenchidos.")
                };
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate,
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token, UserEntity user)
        {
            return new
            {
                authentication = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                token = "Bearer " + token,
                name = user.Name,
                username = user.Username,
                message = "Utilizador autenticado com sucesso"
            };
        }
    }
}
