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
using Warbud.Users.Services;

namespace Warbud.Users.GqlControllers.User
{
    [ExtendObjectType(nameof(Query))]
    public class UserGql : GqlBase, IGqlOperation
    {
        private readonly IUserContextService _userContextService;
        private readonly IQueryDispatcher _queryDispatcher;

        public UserGql(IUserContextService userContextService, IQueryDispatcher queryDispatcher)
        {
            _userContextService = userContextService;
            _queryDispatcher = queryDispatcher;
        }

        [Authorize]
        public async Task<ActionResult<UserDto>> Me()
        {
            var userId = _userContextService.GetUserId;
            //TODO
            if (userId is null) throw new ArgumentException("Invalid user id");
            var query = new GetUserById((Guid) userId);
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }

        [Authorize(Roles = new[] {Role.Name.BasicUser})]
        [UsePaging(IncludeTotalCount = true, MaxPageSize = 50)]
        [UseFiltering]
        [UseSorting]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers([FromBody] GetUsers query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }

        [Authorize(Roles = new[] {Role.Name.BasicUser})]
        public async Task<ActionResult<UserDto>> GetUser([FromBody] GetUserById query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }

        [Authorize(Policy = Policy.Name.VerifiedUser)]
        public async Task<ActionResult<UserDto>> GetUsersById([FromBody] GetUserById query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }

        public async Task<ActionResult<string>> Login([FromBody] LoginUser query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }
    }
}