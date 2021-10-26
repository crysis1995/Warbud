using System.Linq;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Warbud.Users.Infrastructure.Data;
using Warbud.Users.Types.Inputs;

namespace Warbud.Users.Validators
{
    public class ExternalUserInputValidator : AbstractValidator<AddExternalUserInput>
    {
        public ExternalUserInputValidator(IDbContextFactory<UserDbContext> dbContextFactory)
        {
            var dbContext = dbContextFactory.CreateDbContext();

            RuleFor(input => input.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(input => input.Password)
                .MinimumLength(8);

            RuleFor(input => input.ConfirmPassword)
                .Equal(input => input.Password);

            RuleFor(input => input.Email).Custom((value, context) =>
            {
                var emailInUse = dbContext.ExternalUsers.Any(x => x.Email == value);
                if (emailInUse)
                {
                    context.AddFailure("Email", "There is already user with given email");
                }
            });
        }
    }
}