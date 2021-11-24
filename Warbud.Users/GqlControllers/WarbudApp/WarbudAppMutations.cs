using System.Threading.Tasks;
using FluentValidation;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using Warbud.Shared.Abstraction.Commands;
using Warbud.Shared.Abstraction.Constants;
using Warbud.Shared.Abstraction.Markers;
using Warbud.Users.Application.Commands.WarbudApp;

namespace Warbud.Users.GqlControllers.WarbudApp
{
    [ExtendObjectType(nameof(Mutation))]
    public class AppMutation: IGqlOperation
    {
        private readonly IValidator<AddWarbudApp> _appValidator;
        private readonly ICommandDispatcher _commandDispatcher;

        public AppMutation(IValidator<AddWarbudApp> appValidator, ICommandDispatcher commandDispatcher)
        {
            _appValidator = appValidator;
            _commandDispatcher = commandDispatcher;
        }

        [Authorize(Roles = new []{Role.Name.Admin})]
        public async Task AddAppAsync(AddWarbudApp command)
        {
            await _appValidator.ValidateAndThrowAsync(command);
            await _commandDispatcher.DispatchAsync(command);
        }
        
        [Authorize(Roles = new []{ Role.Name.Admin})]
        public async Task UpdateAppAsync(UpdateWarbudApp command)
        {
            await _commandDispatcher.DispatchAsync(command);
        }
        
        [Authorize(Roles = new []{ Role.Name.Admin})]
        public async Task DeleteAppAsync(RemoveWarbudApp command)
        {
            await _commandDispatcher.DispatchAsync(command);
        }
    }
}