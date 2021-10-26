using System.Linq;
using System.Reflection.Metadata;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Warbud.Users.Database.Models;
using Warbud.Users.Infrastructure.Data;

namespace Warbud.Users.GqlControllers
{
    public class Query
    {
        [UseDbContext(typeof(UserDbContext))]
        [UsePaging(IncludeTotalCount = true, MaxPageSize = 50)]
        [UseFiltering]
        [UseSorting]
        public IQueryable<ExternalUser> GetExternalUsers([ScopedService] UserDbContext context)
        {
            return context.ExternalUsers;
        }
    }
}