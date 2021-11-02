using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Warbud.Users.Authentication;
using Warbud.Users.Constants;
using Warbud.Users.Database.Models;
using Warbud.Users.Infrastructure.Data;
using Warbud.Users.Services;
using Warbud.Users.Types.Inputs;

namespace Warbud.Users.GqlControllers
{
    public class Query
    {
        private readonly IPasswordHasher<ExternalUser> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IUserContextService _userContextService;

        public Query(IPasswordHasher<ExternalUser> passwordHasher,
            AuthenticationSettings authenticationSettings,
            IUserContextService userContextService)
        {
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _userContextService = userContextService;
        }

        [Authorize(Roles = new[] {Claims.RoleValues.Admin})]
        [UseDbContext(typeof(UserDbContext))]
        [UsePaging(IncludeTotalCount = true, MaxPageSize = 50)]
        [UseFiltering]
        [UseSorting]
        public IQueryable<ExternalUser> GetExternalUsers([ScopedService] UserDbContext context)
        {
            return context.ExternalUsers;
        }

        [Authorize] //TODO Kto moze pobierac innych po ID? //Domyslnie mozna zrobic zapytanie pod /Me BasicUser+ ale pytanie tylko o czesc danych jak nie jestes adminem
        [UseDbContext(typeof(UserDbContext))]
        public async Task<ExternalUser> GetExternalUsersById(Guid id, [ScopedService] UserDbContext context)
        {
            return await context.ExternalUsers.FindAsync(id);
        }

        [Authorize]
        [UseDbContext(typeof(UserDbContext))]
        public async Task<ExternalUser> Me([ScopedService] UserDbContext context)
        {
            var userId = _userContextService.GetUserId;
            if (userId is null) throw new ArgumentException("Invalid user id");
            return await context.ExternalUsers.FindAsync(userId);
        }

        //TODO refactor
        [UseDbContext(typeof(UserDbContext))]
        public string Login(LoginExternalUserInput input, [ScopedService] UserDbContext context)
        {
            var (email, password) = input;
            var user = context.ExternalUsers.FirstOrDefault(x => x.Email == email);
            if (user is null) return "Invalid username or password";
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (result == PasswordVerificationResult.Failed) return "Invalid username or password";

            var claims = new List<Claim>
            {
                new(Claims.ClaimsNames.Id, user.Id.ToString()),
                new(Claims.ClaimsNames.Role, $"{user.Role.ToString()}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddSeconds(_authenticationSettings.JwtExpireSeconds);

            var token = new JwtSecurityToken(
                _authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        [Authorize(Roles = new[] {Claims.RoleValues.BasicUser, Claims.RoleValues.Admin})]
        [UseDbContext(typeof(UserDbContext))]
        public async Task<WarbudApp> GetAppById(int id, [ScopedService] UserDbContext context)
        {
            return await context.WarbudApps.FindAsync(id);
        }

        [Authorize(Roles = new[] {Claims.RoleValues.BasicUser, Claims.RoleValues.Admin})]
        [UseDbContext(typeof(UserDbContext))]
        [UsePaging(IncludeTotalCount = true, MaxPageSize = 50)]
        [UseFiltering]
        [UseSorting]
        public IQueryable<WarbudApp> GetApps([ScopedService] UserDbContext context)
        {
            return context.WarbudApps;
        }

        [Authorize(Roles = new[] {Claims.RoleValues.BasicUser, Claims.RoleValues.Admin})]
        [UseDbContext(typeof(UserDbContext))]
        public async Task<WarbudClaim> GetClaimById(GetWarbudClaimInput input, [ScopedService] UserDbContext context)
        {
            var (userId, appId, projectId) = input;
            return await context.WarbudClaims.FindAsync(userId, appId, projectId);
        }

        // TODO Get all my WarbudClaims
        //[Authorize(Roles = new[] {Claims.RoleValues.BasicUser, Claims.RoleValues.Admin})]
        [Authorize(Policy = Policy.PolicyNames.DoILikeYou)]
        [UseDbContext(typeof(UserDbContext))]
        [UsePaging(IncludeTotalCount = true, MaxPageSize = 50)]
        [UseFiltering]
        [UseSorting]
        public IQueryable<WarbudClaim> GetAllWarbudClaimByUserId(Guid id, [ScopedService] UserDbContext context)
        {
            return context.WarbudClaims.Where(x => x.UserId == id);
        }

        [Authorize(Roles = new[] {Claims.RoleValues.Admin})]
        public List<string> GetClaimsName()
        {
            return Claims.ClaimValues.GetValueList();
        }
    }
}