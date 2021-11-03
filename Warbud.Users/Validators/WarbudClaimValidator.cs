using System.Linq;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Warbud.Shared.Constants;
using Warbud.Users.Infrastructure.Data;
using Warbud.Users.Types.Inputs;

namespace Warbud.Users.Validators
{
    public class WarbudClaimInputValidator : AbstractValidator<AddWarbudClaimInput>
    {
        public WarbudClaimInputValidator(IDbContextFactory<UserDbContext> dbContextFactory)
        {
            var dbContext = dbContextFactory.CreateDbContext();
            
            RuleFor(input => input.UserId).Custom((value, context) =>
            {
                var user = dbContext.ExternalUsers.Any(x => x.Id == value);
                if (!user)
                {
                    context.AddFailure("UserId", "There is no user with given id");
                }
            });
            
            RuleFor(input => input.AppId).Custom((value, context) =>
            {
                var app = dbContext.WarbudApps.Any(x => x.Id == value);
                if (!app)
                {
                    context.AddFailure("AppId", "There is no app with given id");
                }
            });
            
           RuleFor(input => input.Name).NotEmpty().NotNull().Length(2, 50).Custom((name, context) =>
           {
               var claimValue = Claim.Value.GetValueList();

               if (claimValue.All(x => x != name))
               {
                   context.AddFailure("Name", "Claim name value");
               }
           });
        }
    }
}