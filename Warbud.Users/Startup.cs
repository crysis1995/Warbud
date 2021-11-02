using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Warbud.Users.Authentication;
using Warbud.Users.Constants;
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
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policy.PolicyNames.DoILikeYou, builder =>
                    builder.RequireClaim(Claims.ClaimsNames.Role, 
                        Claims.RoleValues.Admin, 
                        Claims.RoleValues.BasicUser));
                options.AddPolicy(Policy.PolicyNames.AdminOrOwner,
                    builder => builder.AddRequirements(new ResourceOperationRequirement(ResourceOperation.Update)));
                // options.AddPolicy(Policy.PolicyNames.DoILikeYou,
                //     builder => builder.AddRequirements(new DoILikeYouRequirements(true)));
                
            });

            //services.AddScoped<IAuthorizationHandler, DoILikeYouRequirementsHandler>();
            services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();

            services.AddScoped<IUserContextService, UserContextService>();
            services.AddHttpContextAccessor();
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