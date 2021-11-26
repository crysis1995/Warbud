using System.Collections.Generic;
using System.Threading.Tasks;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using Warbud.Shared.Abstraction.Constants;
using Warbud.Shared.Abstraction.Markers;
using Warbud.Shared.Abstraction.Queries;
using Warbud.Users.Application.DTO;
using Warbud.Users.Application.Queries.WarbudApp;

namespace Warbud.Users.GqlControllers.WarbudApp
{
    [ExtendObjectType(nameof(Query))]
    public class WarbudAppQuery : GqlQueryBase, IGqlOperation
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public WarbudAppQuery(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        [Authorize(Policy = Policy.Name.VerifiedUser)]
        public async Task<WarbudAppDto> GetApp(GetWarbudApp query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFoundGql(result);
        }

        [Authorize(Policy = Policy.Name.VerifiedUser)]
        [UsePaging(IncludeTotalCount = true, MaxPageSize = 50)]
        [UseFiltering]
        [UseSorting]
        public async Task<IEnumerable<WarbudAppDto>> GetApps()
        {
            var result = await _queryDispatcher.QueryAsync(new GetWarbudApps());
            return OkOrNotFoundGql(result);
        }
    }
}