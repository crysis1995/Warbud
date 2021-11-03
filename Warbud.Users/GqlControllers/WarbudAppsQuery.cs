using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using Warbud.Shared.Constants;
using Warbud.Users.Database.Models;
using Warbud.Users.Infrastructure.Data;

namespace Warbud.Users.GqlControllers
{
    public partial class Query
    {
        [Authorize(Policy = Policy.Name.VerifiedUser)]
        [UseDbContext(typeof(UserDbContext))]
        public async Task<WarbudApp> GetAppById(int id, [ScopedService] UserDbContext context)
        {
            return await context.WarbudApps.FindAsync(id);
        }

        [Authorize(Policy = Policy.Name.VerifiedUser)]
        [UseDbContext(typeof(UserDbContext))]
        [UsePaging(IncludeTotalCount = true, MaxPageSize = 50)]
        [UseFiltering]
        [UseSorting]
        public IQueryable<WarbudApp> GetApps([ScopedService] UserDbContext context)
        {
            return context.WarbudApps;
        }
    }
}