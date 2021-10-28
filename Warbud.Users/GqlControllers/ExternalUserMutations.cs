using System;
using System.Threading.Tasks;
using FluentValidation;
using HotChocolate;
using HotChocolate.Data;
using Warbud.Users.Database.Models;
using Warbud.Users.Helpers;
using Warbud.Users.Infrastructure.Data;
using Warbud.Users.Types.Inputs;
using Warbud.Users.Types.Payloads;

namespace Warbud.Users.GqlControllers
{
    public partial class Mutations
    {
        [UseDbContext(typeof(UserDbContext))]
        public async Task<ExternalUserPayload> AddExternalUserAsync(AddExternalUserInput input,
            [ScopedService] UserDbContext context)
        {
            await _userValidator.ValidateAndThrowAsync(input);

            var (firstName, lastName, password, email, _) = input;
            var user = new ExternalUser
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email
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
            var user = await context.ExternalUsers.FindAsync(input.Id);
            if (user is null) throw new ArgumentException("There is no user with given Id");
            user.UpdateEntity(input);
            await context.SaveChangesAsync().ConfigureAwait(false);
            return new UserPayload(user);
        }

        [UseDbContext(typeof(UserDbContext))]
        public async Task<UserPayload> UpdateUserRoleAsync(UpdateUserRoleInput input,
            [ScopedService] UserDbContext context)
        {
            var (guid, role) = input;
            var user = await context.ExternalUsers.FindAsync(guid);
            if (user is null) throw new ArgumentException("There is no user with given Id");
            user.Role = role;
            await context.SaveChangesAsync().ConfigureAwait(false);
            return new UserPayload(user);
        }

        [UseDbContext(typeof(UserDbContext))]
        public async Task<bool> DeleteUserAsync(Guid id, [ScopedService] UserDbContext context)
        {
            var user = await context.ExternalUsers.FindAsync(id);
            if (user is null) return false;
            context.ExternalUsers.Remove(user);
            await context.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }
    }
}