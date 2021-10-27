using System;
using System.Threading.Tasks;
using FluentValidation;
using System.Linq;
using HotChocolate;
using HotChocolate.Data;
using Warbud.Users.Database.Models;
using Warbud.Users.Infrastructure.Data;
using Warbud.Users.Types.Inputs;
using Warbud.Users.Types.Payloads;
using Microsoft.AspNetCore.Identity;
using Warbud.Users.Helpers;

namespace Warbud.Users.GqlControllers
{
    public partial class Mutations
    {
        private readonly IPasswordHasher<ExternalUser> _passwordHasher;
        private readonly IValidator<AddExternalUserInput> _userValidator;

        public Mutations(IPasswordHasher<ExternalUser> passwordHasher, IValidator<AddExternalUserInput> userUserValidator, IValidator<AddWarbudAppInput> appValidator)
        {
            _passwordHasher = passwordHasher;
            _userValidator = userUserValidator;
            _appValidator = appValidator;
        }
        
        [UseDbContext(typeof(UserDbContext))]
        public async Task<ExternalUserPayload> AddExternalUserAsync(AddExternalUserInput input, [ScopedService] UserDbContext context)
        {
            await _userValidator.ValidateAndThrowAsync(input);
            
            var (firstName, lastName, password, email, _) = input;
            var user = new ExternalUser()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
            };

            var hashPassword = _passwordHasher.HashPassword(user, password);
            user.PasswordHash = hashPassword;
            context.ExternalUsers.Add(user);
            try
            {
                await context.SaveChangesAsync().ConfigureAwait(false);
                return new ExternalUserPayload(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to add new external user: {ex.Message}", ex);
            }
        }
        
        [UseDbContext(typeof(UserDbContext))]
        public async Task<UserPayload> UpdateUserAsync(UpdateUserInput input, [ScopedService] UserDbContext context)
        {
            var user = context.ExternalUsers.FirstOrDefault(x => x.Id == input.Id);
            if (user is null)
            {
                throw new ArgumentException("There is no user with given Id");
            }
            user.UpdateEntity(input);
            await context.SaveChangesAsync().ConfigureAwait(false);
            return new UserPayload(user);
        }
        
        [UseDbContext(typeof(UserDbContext))]
        public async Task<UserPayload> UpdateUserRoleAsync(UpdateUserRoleInput input, [ScopedService] UserDbContext context)
        {
            var (guid, role) = input;
            var user = context.ExternalUsers.FirstOrDefault(x => x.Id == guid);
            if (user is null)
            {
                throw new ArgumentException("There is no user with given Id");
            }
            user.Role = role;
            await context.SaveChangesAsync().ConfigureAwait(false);
            return new UserPayload(user);
        }
        
        [UseDbContext(typeof(UserDbContext))]
        public async Task<bool> DeleteUserAsync(Guid id, [ScopedService] UserDbContext context)
        {
            ExternalUser user = context.ExternalUsers.SingleOrDefault(x => x.Id == id);
            if (user is null)
            {
                return false;
            }
            context.ExternalUsers.Remove(user);
            await context.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }
    }
}