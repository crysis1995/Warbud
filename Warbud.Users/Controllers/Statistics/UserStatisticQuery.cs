// using System.Linq;
// using HotChocolate.AspNetCore.Authorization;
// using HotChocolate.Data;
// using HotChocolate.Types;
// using Warbud.Shared.Constants;
// using Warbud.Users.Domain.Entities;
//
// namespace Warbud.Users.GqlControllers
// {
//     public class StatisticsQuery
//     {
//         [Authorize(Roles = new []{ Role.Name.Admin})]
//         [UsePaging(IncludeTotalCount = true, MaxPageSize = 50)]
//         [UseFiltering]
//         [UseSorting]
//         public IQueryable<UserStatistic> GetUsersStatistic()
//         {
//             return context.UserStatistics;
//         }
//     }
// }