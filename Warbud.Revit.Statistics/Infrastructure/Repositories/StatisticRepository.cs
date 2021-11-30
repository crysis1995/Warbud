using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Warbud.Revit.Statistics.Entities;
using Warbud.Revit.Statistics.Infrastructure.Context;
using Warbud.Revit.Statistics.Interfaces;

namespace Warbud.Revit.Statistics.Infrastructure.Repositories
{
    internal class StatisticRepository : IStatisticRepository
    {
        private readonly StatisticContext _context;
        private readonly DbSet<Statistic> _statistics;

        public StatisticRepository(StatisticContext context)
        {
            _context = context;
            _statistics = context.Statistics;
        }

        public Task<Statistic> GetAsync(int id)
            => _statistics.SingleOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(Statistic statistic)
        {
            await _statistics.AddAsync(statistic);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Statistic statistic)
        {
            _statistics.Remove(statistic);
            await _context.SaveChangesAsync();
        }
    }
}