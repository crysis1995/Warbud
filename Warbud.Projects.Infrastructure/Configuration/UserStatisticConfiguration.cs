using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warbud.Projects.Database.Models;

namespace Warbud.Projects.Infrastructure.Configuration
{
    public class WarbudProjectConfiguration : IEntityTypeConfiguration<WarbudProject>
    {

        public void Configure(EntityTypeBuilder<WarbudProject> builder)
        {
            
            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
        }
    }
}