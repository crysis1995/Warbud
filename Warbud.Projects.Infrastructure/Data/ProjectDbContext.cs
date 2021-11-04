using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Warbud.Projects.Database.Models;
using Warbud.Projects.Infrastructure.Configuration;
using Warbud.Shared.Abstraction;

namespace Warbud.Projects.Infrastructure.Data
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {
        }
        
        

        public DbSet<WarbudProject> ExternalUsers { get; set; }

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
            builder.ApplyConfiguration(new WarbudProjectConfiguration());
        }
    }
}