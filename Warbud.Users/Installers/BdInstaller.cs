using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warbud.Users.Infrastructure.Data;

namespace Warbud.Users.Installers
{
    public class BdInstaller : IInstaller
    {
        public void InstallServices( IServiceCollection services, IConfiguration configuration)
        {
            services.AddPooledDbContextFactory<UserDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("WarbudUserCS")).EnableSensitiveDataLogging());
        }
    }
}