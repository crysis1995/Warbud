using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Warbud.Users.Authentication;
using Warbud.Shared.Constants;
using Warbud.Users.Installers;
using Warbud.Users.Services;

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
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddHttpContextAccessor();
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policy.Name.VerifiedUser,
                    policy => policy.Requirements.Add(new VerifiedUserRequirement()));
                options.AddPolicy(Policy.Name.AdminOrOwner,
                    policy => policy.Requirements.Add(new AdminOrOwnerRequirement()));
            });
            
            services.AddSingleton<IAuthorizationHandler, VerifiedUserRequirementsHandler>();
            services.AddSingleton<IAuthorizationHandler, AdminOrOwnerRequirementHandler>();

            
            services.InstallServicesInAssembly(_config);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapGraphQL());
            app.UseGraphQLVoyager();
        }
    }
}