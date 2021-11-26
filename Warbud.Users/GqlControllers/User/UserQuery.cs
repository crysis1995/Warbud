using System;
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
using Warbud.Users.Application.Queries.User;
using Warbud.Users.Exceptions;
using Warbud.Users.Services;

namespace Warbud.Users.GqlControllers.User
{
    [ExtendObjectType(nameof(Query))]
    public class UserQuery : GqlQueryBase, IGqlOperation
    {
        private readonly IUserContextService _userContextService;
        private readonly IQueryDispatcher _queryDispatcher;

        public UserQuery(IUserContextService userContextService, IQueryDispatcher queryDispatcher)
        {
            _userContextService = userContextService;
            _queryDispatcher = queryDispatcher;
        }

        [Authorize]
        public async Task<UserDto> Me()
        {
            var userId = _userContextService.GetUserId;
            if (userId is null) throw new InvalidTokenException();
            var query = new GetUserById((Guid) userId);
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFoundGql(result);
        }

        [Authorize(Policy = Policy.Name.VerifiedUser)]
        [UsePaging(IncludeTotalCount = true, MaxPageSize = 50)]
        [UseFiltering]
        [UseSorting]
        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var result = await _queryDispatcher.QueryAsync(new GetUsers());
            return OkOrNotFoundGql(result);
        }

        [Authorize(Policy = Policy.Name.VerifiedUser)]
        public async Task<UserDto> GetUser([FromBody] GetUserById query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFoundGql(result);
        }

        public async Task<string> Login([FromBody] LoginUser query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFoundGql(result);
        }
    }
}