using System;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using Warbud.Users.Database.Models;
using Warbud.Users.Infrastructure.Data;
using Warbud.Users.Types.Inputs;
using Warbud.Users.Types.Payloads;
using System.Linq;
using Warbud.Users.Helpers;

namespace Warbud.Users.GqlControllers
{
    public partial class Mutations
    {
        [UseDbContext(typeof(UserDbContext))]
        public async Task<ExternalUserPayload> AddExternalUserAsync(AddExternalUserInput input, [ScopedService] UserDbContext context)
        {
            var (firstName, lastName, role) = input;
            var user = new ExternalUser()
            {
                FirstName = firstName,
                LastName = lastName,
                Role = role
            };

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
        public async Task<ExternalUserPayload> UpdateExternalUserAsync(UpdateExternalUserInput input, [ScopedService] UserDbContext context)
        {
            var user = context.ExternalUsers.SingleOrDefault(x => x.Id == input.Id);

            if (user is null)
            {
                throw new ArgumentException("There is no user with given Id");
            }
            user.UpdateEntity(input);
            await context.SaveChangesAsync().ConfigureAwait(false);
            return new ExternalUserPayload(user);
        }
        [UseDbContext(typeof(UserDbContext))]
        public async Task<bool> DeleteExternalUserAsync(Guid id, [ScopedService] UserDbContext context)
        {
            var user = context.ExternalUsers.SingleOrDefault(x => x.Id == id);

            if (user is null) return false;
            
            context.ExternalUsers.Remove(user);
            await context.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }
    }
}