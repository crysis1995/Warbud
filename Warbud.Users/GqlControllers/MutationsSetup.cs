using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Warbud.Users.Database.Models;
using Warbud.Users.Types.Inputs;

namespace Warbud.Users.GqlControllers
{
    public partial class Mutations
    {
        
        private readonly IPasswordHasher<ExternalUser> _passwordHasher;
        private readonly IValidator<AddExternalUserInput> _userValidator;
        private readonly IValidator<AddWarbudAppInput> _appValidator;
        private readonly IValidator<AddWarbudClaimInput> _claimValidator;
        public Mutations(IPasswordHasher<ExternalUser> passwordHasher,
            IValidator<AddExternalUserInput> userUserValidator,
            IValidator<AddWarbudAppInput> appValidator,
            IValidator<AddWarbudClaimInput> claimValidator)
        {
            _passwordHasher = passwordHasher;
            _userValidator = userUserValidator;
            _appValidator = appValidator;
            _claimValidator = claimValidator;
        }
    }
}