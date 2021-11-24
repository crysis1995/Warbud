using System.Threading.Tasks;
using FluentValidation;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using Warbud.Shared.Abstraction.Commands;
using Warbud.Shared.Abstraction.Constants;
using Warbud.Shared.Abstraction.Markers;
using Warbud.Users.Application.Commands.User;

namespace Warbud.Users.GqlControllers.User
{
    [ExtendObjectType(nameof(Mutation))]
    public class UserMutation: IGqlOperation
    {
        private readonly IValidator<AddUser> _userValidator;
        private readonly ICommandDispatcher _commandDispatcher;

        public UserMutation(IValidator<AddUser> userValidator,
            ICommandDispatcher commandDispatcher)
        {
            _userValidator = userValidator;
            _commandDispatcher = commandDispatcher;
        }

        public async Task AddUserAsync(AddUser command)
        {
            await _userValidator.ValidateAndThrowAsync(command);
            await _commandDispatcher.DispatchAsync(command);
        }

        [Authorize(Policy = Policy.Name.AdminOrOwner)]
        public async Task UpdateUserAsync(UpdateUser command)
        {
            await _commandDispatcher.DispatchAsync(command);
        }

        [Authorize(Roles = new []{ Role.Name.Admin})]
        public async Task UpdateUserRoleAsync(UpdateUserRole command)
        {
            await _commandDispatcher.DispatchAsync(command);
        }

        [Authorize(Roles = new []{Role.Name.Admin})]
        public async Task DeleteUserAsync(RemoveUser command)
        {
            await _commandDispatcher.DispatchAsync(command);
        }
    }
}