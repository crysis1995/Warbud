using System.Threading.Tasks;
using FluentValidation;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using Warbud.Shared.Abstraction.Commands;
using Warbud.Shared.Abstraction.Constants;
using Warbud.Shared.Abstraction.Markers;
using Warbud.Users.Application.Commands.WarbudClaim;

namespace Warbud.Users.GqlControllers.WarbudClaim
{
    [ExtendObjectType(nameof(Mutation))]
    public class ClaimMutation: IGqlOperation
    {
        private readonly IValidator<AddWarbudClaim> _claimValidator;
        private readonly ICommandDispatcher _commandDispatcher;

        public ClaimMutation(IValidator<AddWarbudClaim> claimValidator,
            ICommandDispatcher commandDispatcher)
        {
            _claimValidator = claimValidator;
            _commandDispatcher = commandDispatcher;
        }


        [Authorize(Roles = new []{ Role.Name.Admin})]
        public async Task AddClaimAsync(AddWarbudClaim command)
        {
            await _claimValidator.ValidateAsync(command);
            await _commandDispatcher.DispatchAsync(command);
        }

        [Authorize(Roles = new []{Role.Name.Admin})]
        public async Task UpdateClaimAsync(UpdateWarbudClaim command)
        {
            await _commandDispatcher.DispatchAsync(command);
        }

        [Authorize(Roles = new []{ Role.Name.Admin})]
        public async Task DeleteClaimAsync(RemoveWarbudClaim command)
        {
            await _commandDispatcher.DispatchAsync(command);
        }
    }
}