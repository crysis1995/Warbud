using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Warbud.Users.Authentication;
using Warbud.Users.Constants;
using Warbud.Users.Database.Models;
using Warbud.Users.Infrastructure.Data;
using Warbud.Users.Types.Inputs;

namespace Warbud.Users.GqlControllers
{
    public class Query
    {
        private readonly IPasswordHasher<ExternalUser> _passwordHasher;        
        private readonly AuthenticationSettings _authenticationSettings;

        public Query(IPasswordHasher<ExternalUser> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        [UseDbContext(typeof(UserDbContext))]
        [UsePaging(IncludeTotalCount = true, MaxPageSize = 50)]
        [UseFiltering]
        [UseSorting]
        public IQueryable<ExternalUser> GetExternalUsers([ScopedService] UserDbContext context)
        {
            return context.ExternalUsers;
        }
        
        [UseDbContext(typeof(UserDbContext))]
        public async Task<ExternalUser> GetExternalUsersById(Guid id, [ScopedService] UserDbContext context)
        {
            return await context.ExternalUsers.FindAsync(id);
        }
        
        [UseDbContext(typeof(UserDbContext))]
        public string Login(LoginExternalUserInput input, [ScopedService] UserDbContext context)
        {
            var (email, password) = input;
            var user = context.ExternalUsers.FirstOrDefault(x => x.Email == email);
            if (user is null)
            {
                return "Invalid username or password";
            }
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (result == PasswordVerificationResult.Failed)
            {
                return "Invalid username or password";
            }
            
            var claims = new List<Claim>()
            {
                new Claim(Claims.Names.Id, user.Id.ToString()),
                new Claim(Claims.Names.Role, $"{user.Role.ToString()}"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes((_authenticationSettings.JwtExpireMinutes));

            var token = new JwtSecurityToken(
                _authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return  tokenHandler.WriteToken(token);
        }
        
        [UseDbContext(typeof(UserDbContext))]
        public async Task<WarbudApp> GetAppById(int id, [ScopedService] UserDbContext context)
        {
            return await context.WarbudApps.FindAsync(id);
        }
        
        [UseDbContext(typeof(UserDbContext))]
        [UsePaging(IncludeTotalCount = true, MaxPageSize = 50)]
        [UseFiltering]
        [UseSorting]
        public IQueryable<WarbudApp> GetApps([ScopedService] UserDbContext context)
        {
            return context.WarbudApps;
        }
        
        [UseDbContext(typeof(UserDbContext))]
        public async Task<WarbudClaim> GetClaimById(GetWarbudClaimInput input, [ScopedService] UserDbContext context)
        {
            var (userId, appId, projectId) = input;
            return await context.WarbudClaims.FindAsync(userId, appId, projectId);
        }
        
        [UseDbContext(typeof(UserDbContext))]
        [UsePaging(IncludeTotalCount = true, MaxPageSize = 50)]
        [UseFiltering]
        [UseSorting]
        public IQueryable<WarbudClaim> GetAllClaimByUserId(Guid id, [ScopedService] UserDbContext context)
        {
            return  context.WarbudClaims.Where(x => x.UserId == id);
        }

        public List<string> GetClaimsName()
        {
            return Claims.RoleValues.GetValueList();
        }
    }
}