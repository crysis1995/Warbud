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
        public async Task<bool> DeleteUserAsync(Guid id, [ScopedService] UserDbContext context)
        {
            ExternalUser user = context.ExternalUsers.SingleOrDefault(x => x.Id == id);
            if (user is null)
            {
                return false;
            }
            context.ExternalUsers.Remove((ExternalUser)user);
            await context.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }
    }
}