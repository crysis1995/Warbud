using System.Linq;
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
        [Authorize(Roles = new []{ Role.Name.Admin})]
        [UseDbContext(typeof(UserDbContext))]
        [UsePaging(IncludeTotalCount = true, MaxPageSize = 50)]
        [UseFiltering]
        [UseSorting]
        public IQueryable<UserStatistic> GetUsersStatistic([ScopedService] UserDbContext context)
        {
            return context.UserStatistics;
        }
    }
}