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
using Warbud.Users.Application.Queries.WarbudApp;

namespace Warbud.Users.GqlControllers.WarbudApp
{
    [ExtendObjectType(nameof(Query))]
    public class WarbudAppGql : GqlBase, IGqlOperation
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public WarbudAppGql(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        [Authorize(Policy = Policy.Name.VerifiedUser)]
        public async Task<ActionResult<WarbudAppDto>> GetAppById(GetWarbudApp query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }

        [Authorize(Policy = Policy.Name.VerifiedUser)]
        [UsePaging(IncludeTotalCount = true, MaxPageSize = 50)]
        [UseFiltering]
        [UseSorting]
        public async Task<ActionResult<IEnumerable<WarbudAppDto>>> GetApps(GetWarbudApps query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }
    }
}