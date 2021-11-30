using Microsoft.EntityFrameworkCore;
using Warbud.Revit.Statistics.Entities;
using Warbud.Revit.Statistics.Infrastructure.Configurations;

namespace Warbud.Revit.Statistics.Infrastructure.Context
{
    public class StatisticContext: DbContext
    {
        public StatisticContext(DbContextOptions<StatisticContext> options) : base(options)
        {
        }
        
        public DbSet<Statistic> Statistics { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("warbud.revit.statistics");

            var configuration = new StatisticConfiguration();
            modelBuilder.ApplyConfiguration(configuration);
        }
    }
}