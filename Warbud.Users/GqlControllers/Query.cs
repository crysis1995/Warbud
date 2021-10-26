﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Warbud.Users.Authentication;
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
        public string Login(LoginExternalUserInput input, [ScopedService] UserDbContext context)
        {
            var user = context.ExternalUsers.FirstOrDefault(x => x.Email == input.Email);
            if (user is null)
            {
                return "Invalid username or password";
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, input.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return "Invalid username or password";
            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, $"{user.Role.ToString()}"),
                //new Claim("ObjectTypeClaim", new ClaimObject("test", 1, user.Role.ToString()) ),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays((_authenticationSettings.JwtExpireDays));

            var token = new JwtSecurityToken(
                _authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return  tokenHandler.WriteToken(token);
        }
    }

    internal record ClaimObject(string Name, int Number, string Role);
}