using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warbud.Users.Types.Inputs;
using Warbud.Users.Validators;

namespace Warbud.Users.Installers
{
    public class ValidationInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentValidation();
            services.AddTransient<IValidator<AddExternalUserInput>, ExternalUserInputValidator>();
            services.AddTransient<IValidator<AddWarbudAppInput>, WarbudAppInputValidator>();
            services.AddTransient<IValidator<AddWarbudClaimInput>, WarbudClaimInputValidator>();
        }
    }
}