using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Warbud.Shared.Constants;
using Warbud.Users.Database.Models;
using Warbud.Users.Infrastructure.Data;
using Warbud.Users.Types.Inputs;

namespace Warbud.Users.GqlControllers
{
    public partial class Query
    {
        [Authorize]
        [UseDbContext(typeof(UserDbContext))]
        public async Task<ExternalUser> Me([ScopedService] UserDbContext context)
        {
            var userId = _userContextService.GetUserId;
            if (userId is null) throw new ArgumentException("Invalid user id");
            return await context.ExternalUsers.FindAsync(userId);
        }
        
        [Authorize(Roles = new[] {Role.Name.Admin})]
        [UseDbContext(typeof(UserDbContext))]
        [UsePaging(IncludeTotalCount = true, MaxPageSize = 50)]
        [UseFiltering]
        [UseSorting]
        public IQueryable<ExternalUser> GetExternalUsers([ScopedService] UserDbContext context)
        {
            return context.ExternalUsers;
        }

        [Authorize(Policy = Policy.Name.VerifiedUser)]
        [UseDbContext(typeof(UserDbContext))]
        public async Task<ExternalUser> GetExternalUsersById(Guid id, [ScopedService] UserDbContext context)
        {
            return await context.ExternalUsers.FindAsync(id);
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

            var claims = new List<System.Security.Claims.Claim>
            {
                new(Claim.Name.Id, user.Id.ToString()),
                new(Claim.Name.Role, $"{user.Role.ToString()}")
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
        
    }
}