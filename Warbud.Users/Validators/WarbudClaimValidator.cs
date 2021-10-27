using System.Linq;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Warbud.Users.Constants;
using Warbud.Users.Infrastructure.Data;
using Warbud.Users.Types.Inputs;

namespace Warbud.Users.Validators
{
    public class WarbudClaimInputValidator : AbstractValidator<AddWarbudClaimInput>
    {
        public WarbudClaimInputValidator()
        {
           RuleFor(input => input.Name).NotEmpty().NotNull().Length(2, 50).Custom((name, context) =>
           {
               var claimValue = Claims.RoleValues.GetValueList();
               
               
               if (claimValue.All(x => x != name))
               {
                   context.AddFailure("Name", "Claim name value");
               }
           });
        }
    }
}