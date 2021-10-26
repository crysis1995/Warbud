using System;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using Warbud.Users.Database.Models;
using Warbud.Users.Infrastructure.Data;
using Warbud.Users.Types.Inputs;
using Warbud.Users.Types.Payloads;
using Microsoft.AspNetCore.Identity;

namespace Warbud.Users.GqlControllers
{
    public partial class Mutations
    {
        private readonly IPasswordHasher<ExternalUser> _passwordHasher;

        public Mutations(IPasswordHasher<ExternalUser> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }
        
        [UseDbContext(typeof(UserDbContext))]
        public async Task<ExternalUserPayload> AddExternalUserAsync(AddExternalUserInput input, [ScopedService] UserDbContext context)
        {
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
    }
}