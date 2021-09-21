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
        public async Task<InternalUserPayload> AddInternalUserAsync(AddInternalUserInput input, [ScopedService] UserDbContext context)
        {
            var user = new InternalUser()
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                Role = input.Role
            };

            context.InternalUsers.Add(user);
            try
            {
                await context.SaveChangesAsync().ConfigureAwait(false);
                return new InternalUserPayload(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to add new external user: {ex.Message}", ex);
            }
        }
        
        [UseDbContext(typeof(UserDbContext))]
        public async Task<InternalUserPayload> UpdateInternalUserAsync(UpdateInternalUserInput input, [ScopedService] UserDbContext context)
        {
            var user = context.InternalUsers.SingleOrDefault(x => x.Id == input.Id);

            if (user is null)
            {
                throw new ArgumentException("There is no user with given Id");
            }
            user.UpdateEntity(input);
            await context.SaveChangesAsync().ConfigureAwait(false);
            return new InternalUserPayload(user);
        }
        [UseDbContext(typeof(UserDbContext))]
        public async Task<bool> DeleteInternalUserAsync(Guid id, [ScopedService] UserDbContext context)
        {
            var user = context.InternalUsers.SingleOrDefault(x => x.Id == id);

            if (user is null) return false;
            
            context.InternalUsers.Remove(user);
            await context.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }
    }
}