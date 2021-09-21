using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Warbud.Users.GqlControllers;
using Warbud.Users.Installers;
using Warbud.Users.Types;

namespace Warbud.Users
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.InstallServicesInAssembly(_config);
            services.AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutations>()
                .AddType<InternalUserType>()
                .AddType<ExternalUserType>()
                .AddFiltering()
                .AddSorting();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }

            app.UseRouting();
            
            app.UseEndpoints(endpoints => endpoints.MapGraphQL());

            app.UseGraphQLVoyager();
        }
    }
}