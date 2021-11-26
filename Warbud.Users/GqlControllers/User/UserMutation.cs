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

        public async Task<bool> AddUserAsync(AddUser command)
        {
            await _userValidator.ValidateAndThrowAsync(command);
            await _commandDispatcher.DispatchAsync(command);
            return true;
        }

        [Authorize(Policy = Policy.Name.AdminOrOwner)]
        public async Task<bool> UpdateUserAsync(UpdateUser command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return true;
        }

        [Authorize(Roles = new []{ Role.Name.Admin})]
        public async Task<bool> UpdateUserRoleAsync(UpdateUserRole command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return true;
        }

        [Authorize(Roles = new []{Role.Name.Admin})]
        public async Task<bool> RemoveUserAsync(RemoveUser command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return true;
        }
    }
}