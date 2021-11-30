using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Warbud.Revit.Statistics.DTO;
using Warbud.Revit.Statistics.Entities;
using Warbud.Revit.Statistics.Infrastructure.Context;
using Warbud.Shared.Abstraction.Queries;

namespace Warbud.Revit.Statistics.Queries.Handlers
{
    public class GetStatisticsHandler :  IQueryHandler<GetStatistics, IEnumerable<StatisticDto>>
    {
        private readonly DbSet<Statistic> _statistics;

        public GetStatisticsHandler(StatisticContext context)
            => _statistics = context.Statistics;
        
        public async Task<IEnumerable<StatisticDto>> HandleAsync(GetStatistics query)
        {
            var dbQuery = _statistics
                .AsQueryable();
            
            return await dbQuery
                .Select(x => x.AsDto())
                .AsNoTracking()
                .ToListAsync();
        }
    }
}