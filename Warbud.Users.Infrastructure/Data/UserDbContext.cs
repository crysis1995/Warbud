using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Warbud.Users.Database.Common;
using Warbud.Users.Database.Models;
using Warbud.Users.Infrastructure.Configuration;

namespace Warbud.Users.Infrastructure.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
            
        }
        public DbSet<ExternalUser> ExternalUsers { get; set; }
        public async Task<int> SaveChangesAsync()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is User && e.State is EntityState.Added or EntityState.Modified);

            foreach (var entityEntry in entries)
            {
                ((User)entityEntry.Entity).LastModified = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    ((User)entityEntry.Entity).Created = DateTime.UtcNow;
                }
            }
            return await base.SaveChangesAsync().ConfigureAwait(false);
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ExternalUserConfiguration());
        }
    }
}