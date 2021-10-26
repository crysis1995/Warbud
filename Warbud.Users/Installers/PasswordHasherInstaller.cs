using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warbud.Users.Database.Models;

namespace Warbud.Users.Installers
{
    public class PasswordHasherInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPasswordHasher<ExternalUser>, PasswordHasher<ExternalUser>>();
        }
    }
}