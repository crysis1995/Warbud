using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warbud.Users.Database.Models;

namespace Warbud.Users.Infrastructure.Configuration
{
    public class WarbudAppConfiguration : IEntityTypeConfiguration<WarbudApp>
    {

        public void Configure(EntityTypeBuilder<WarbudApp> builder)
        {
            
            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.AppName)
                .IsRequired()
                .HasMaxLength(100);
            
            builder
                .Property(x => x.ModuleName)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}