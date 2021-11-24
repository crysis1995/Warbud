using System.Collections.Generic;
using System.Threading.Tasks;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.AspNetCore.Mvc;
using Warbud.Shared.Abstraction.Constants;
using Warbud.Shared.Abstraction.Markers;
using Warbud.Shared.Abstraction.Queries;
using Warbud.Users.Application.DTO;
using Warbud.Users.Application.Queries.WarbudClaim;

namespace Warbud.Users.GqlControllers.WarbudClaim
{
    [ExtendObjectType(nameof(Query))]
    public class WarbudClaimGql : GqlBase, IGqlOperation
    {
        
        private readonly IQueryDispatcher _queryDispatcher;
        public WarbudClaimGql(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }
        
        [Authorize(Policy = Policy.Name.VerifiedUser)]
        public async Task<ActionResult<WarbudClaimDto>> GetClaimById(GetWarbudClaim query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }

        [Authorize(Policy = Policy.Name.AdminOrOwner)]
        [UsePaging(IncludeTotalCount = true, MaxPageSize = 50)]
        [UseFiltering]
        [UseSorting]
        public async Task<ActionResult<IEnumerable<WarbudClaimDto>>> GetAllWarbudClaimByUserId(GetWarbudClaimsByUserId query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }

        [Authorize(Roles = new[] {Role.Name.Admin})]
        public List<string> GetClaimsName()
        {
            return Claim.Value.GetValueList();
        }
    }
}