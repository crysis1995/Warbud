using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using Warbud.Shared.Constants;
using Warbud.Users.Database.Models;
using Warbud.Users.Infrastructure.Data;
using Warbud.Users.Types.Inputs;

namespace Warbud.Users.GqlControllers
{
    public partial class Query
    {
        [Authorize(Policy = Policy.Name.VerifiedUser)]
        [UseDbContext(typeof(UserDbContext))]
        public async Task<WarbudClaim> GetClaimById(GetWarbudClaimInput input, [ScopedService] UserDbContext context)
        {
            var (userId, appId, projectId) = input;
            return await context.WarbudClaims.FindAsync(userId, appId, projectId);
        }

        [Authorize(Policy = Policy.Name.AdminOrOwner)]
        [UseDbContext(typeof(UserDbContext))]
        [UsePaging(IncludeTotalCount = true, MaxPageSize = 50)]
        [UseFiltering]
        [UseSorting]
        public IQueryable<WarbudClaim> GetAllWarbudClaimByUserId(Guid id, [ScopedService] UserDbContext context)
        {
            return context.WarbudClaims.Where(x => x.UserId == id);
        }

        [Authorize(Roles = new[] {Role.Name.Admin})]
        public List<string> GetClaimsName()
        {
            return Claim.Value.GetValueList();
        }
    }
}