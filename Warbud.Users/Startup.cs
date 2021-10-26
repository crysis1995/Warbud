using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Warbud.Users.Database.Models;
using Warbud.Users.GqlControllers;
using Warbud.Users.Infrastructure.Data;
using Warbud.Users.Installers;
using Warbud.Users.Types;
using Warbud.Users.Types.Inputs;
using Warbud.Users.Validators;

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
            //
            // services
            //     .InstallServices(_config)
            //     .AddTransient<IValidator<AddExternalUserInput>, ExternalUserInputValidator>();
            
            //services.AddFluentValidation();
            services.AddScoped<IValidator<AddExternalUserInput>, ExternalUserInputValidator>();
            
            services.AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutations>()
                .AddType<ExternalUserType>()
                .AddFiltering()
                .AddSorting();
            
            services.AddScoped<IPasswordHasher<ExternalUser>, PasswordHasher<ExternalUser>>();
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