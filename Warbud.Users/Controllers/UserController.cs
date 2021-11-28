using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using HotChocolate.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warbud.Shared.Abstraction.Commands;
using Warbud.Shared.Abstraction.Constants;
using Warbud.Shared.Abstraction.Queries;
using Warbud.Users.Application.Commands.User;
using Warbud.Users.Application.DTO;
using Warbud.Users.Application.Queries.User;
using Warbud.Users.Exceptions;
using Warbud.Users.Services;

namespace Warbud.Users.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserContextService _userContextService;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IValidator<AddUser> _userValidator;
        
        public UserController(IUserContextService userContextService,
            IQueryDispatcher queryDispatcher,
            ICommandDispatcher commandDispatcher,
            IValidator<AddUser> userValidator)
        {
            _userContextService = userContextService;
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
            _userValidator = userValidator;
        }
        
        [HttpPost]
        public async Task<bool> AddUserAsync([FromBody] AddUser command)
        {
            await _userValidator.ValidateAndThrowAsync(command);
            await _commandDispatcher.DispatchAsync(command);
            return true;
        }

        [HttpPost]
        [Authorize(Policy = Policy.Name.AdminOrOwner)]
        public async Task<bool> UpdateUserAsync([FromBody] UpdateUser command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return true;
        }

        [HttpPost]
        [Authorize(Roles = new []{ Role.Name.Admin})]
        public async Task<bool> UpdateUserRoleAsync([FromBody] UpdateUserRole command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return true;
        }

        [HttpPost]
        [Authorize(Roles = new []{Role.Name.Admin})]
        public async Task<bool> RemoveUserAsync([FromBody] RemoveUser command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return true;
        }
        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> Me()
        {
            var userId = _userContextService.GetUserId;
            if (userId is null) throw new InvalidTokenException();
            var query = new GetUserById((Guid) userId);
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }

        [HttpGet]
        [Authorize(Policy = Policy.Name.VerifiedUser)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var result = await _queryDispatcher.QueryAsync(new GetUsers());
            return OkOrNotFound(result);
        }

        [HttpGet]
        [Authorize(Policy = Policy.Name.VerifiedUser)]
        public async Task<ActionResult<UserDto>> GetUser([FromBody] GetUserById query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }
        
        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] LoginUser query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(new {token = result});
        }
    }
}