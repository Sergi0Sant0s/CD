using Api.Data.Context;
using Api.Data.Repository;
using Api.Model.IServices.Chat;
using Api.Model.IServices.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Core.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependeciesRepository(IServiceCollection serviceCollection)
        {
            /*
            AddTransient - Cria uma nova instancia (simula um novo objeto)
            AddScoped - Usa a mesma instancia (simula um static)
            AddSingleton - Somente instancia uma vez (singleton)
            */

            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<ILoginRepository, LoginRepository>();
            serviceCollection.AddScoped<IChatRepository, ChatRepository>();

            serviceCollection.AddDbContext<MyContext>(
                options => options.UseSqlServer(@"Server=localhost\sqlexpress;Database=dbApi;Trusted_Connection=True;")
            );

        }
    }
}
