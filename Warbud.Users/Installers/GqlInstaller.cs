using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warbud.Shared.Abstraction.Markers;
using Warbud.Users.GqlControllers;

namespace Warbud.Users.Installers
{
    public class GqlInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var graphqlService = services.AddGraphQLServer();
            graphqlService
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                //.AddType<ExternalUserType>()
                .AddFiltering()
                .AddSorting()
                .AddAuthorization();

            foreach (var type in typeof(Query).Assembly.GetTypes())
                if (type.GetInterface(nameof(IGqlOperation)) != null)
                {
                    services.AddScoped(type);
                    graphqlService.AddTypeExtension(type);
                }
        }
    }
}