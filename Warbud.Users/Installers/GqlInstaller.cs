using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warbud.Users.GqlControllers;
using Warbud.Users.Types;

namespace Warbud.Users.Installers
{
    public class GqlInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutations>()
                .AddType<ExternalUserType>()
                .AddFiltering()
                .AddSorting()
                .AddAuthorization();
        }
    }
}