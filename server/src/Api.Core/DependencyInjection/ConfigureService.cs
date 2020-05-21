using Api.Core.Services.User;
using Api.Model.IServices.Chat;
using Api.Model.IServices.Ftp;
using Api.Model.IServices.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Core.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependeciesService(IServiceCollection serviceCollection)
        {
            /*
            AddTransient - Cria uma nova instancia (simula um novo objeto)
            AddScoped - Usa a mesma instancia (simula um static)
            AddSingleton - Somente instancia uma vez (singleton)
            */
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<ILoginService, LoginService>();
            serviceCollection.AddTransient<IChatService, ChatService>();
            serviceCollection.AddTransient<IFtpManagerService, FtpService>();
        }
    }
}
