using System.Linq;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Warbud.Users.Infrastructure.Data;
using Warbud.Users.Types.Inputs;

namespace Warbud.Users.Validators
{
    public class WarbudAppInputValidator : AbstractValidator<AddWarbudAppInput>
    {
        public WarbudAppInputValidator()
        {
            RuleFor(input => input.AppName).NotEmpty().NotNull().Length(2, 50);
            RuleFor(input => input.ModuleName).NotEmpty().NotNull().Length(2, 50);
        }
    }
}