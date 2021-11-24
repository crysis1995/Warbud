using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Warbud.Shared.Abstraction.Commands;
using Warbud.Users.Application.Commands.User;
using Warbud.Users.Application.Commands.WarbudApp;
using Warbud.Users.Application.Commands.WarbudClaim;
using Warbud.Users.Domain.Entities;

namespace Warbud.Users.GqlControllers
{
    public class Mutation
    {
        
        // private readonly IPasswordHasher<User> _passwordHasher;
        // private readonly IValidator<AddUser> _userValidator;
        // private readonly IValidator<AddWarbudApp> _appValidator;
        // private readonly IValidator<AddWarbudClaim> _claimValidator;
        // private readonly ICommandDispatcher _commandDispatcher;
        //
        // public Mutation(IPasswordHasher<User> passwordHasher,
        //     IValidator<AddUser> userUserValidator,
        //     IValidator<AddWarbudApp> appValidator,
        //     IValidator<AddWarbudClaim> claimValidator,
        //     ICommandDispatcher commandDispatcher)
        // {
        //     _passwordHasher = passwordHasher;
        //     _userValidator = userUserValidator;
        //     _appValidator = appValidator;
        //     _claimValidator = claimValidator;
        //     _commandDispatcher = commandDispatcher;
        // }
    }
}