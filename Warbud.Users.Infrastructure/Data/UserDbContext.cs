using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Warbud.Shared.Abstraction;
using Warbud.Users.Database.Models;
using Warbud.Users.Infrastructure.Configuration;

namespace Warbud.Users.Infrastructure.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        public virtual DbSet<ExternalUser> ExternalUsers { get; set; }
        public virtual DbSet<WarbudApp> WarbudApps { get; set; }
        public virtual DbSet<WarbudClaim> WarbudClaims { get; set; }
        public virtual DbSet<UserStatistic> UserStatistics { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is AuditableEntity && e.State is EntityState.Added or EntityState.Modified);

            foreach (var entityEntry in entries)
            {
                ((AuditableEntity) entityEntry.Entity).LastModified = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                    ((AuditableEntity) entityEntry.Entity).Created = DateTime.UtcNow;
            }

            return await base.SaveChangesAsync().ConfigureAwait(false);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ExternalUserConfiguration());
            builder.ApplyConfiguration(new WarbudClaimConfiguration());
            builder.ApplyConfiguration(new WarbudAppConfiguration());
            builder.ApplyConfiguration(new UserStatisticConfiguration());
        }
    }
}