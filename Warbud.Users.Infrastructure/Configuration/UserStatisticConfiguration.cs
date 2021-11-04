using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warbud.Users.Database.Common;
using Warbud.Users.Database.Models;

namespace Warbud.Users.Infrastructure.Configuration
{
    public class UserStatisticConfiguration : IEntityTypeConfiguration<UserStatistic>
    {

        public void Configure(EntityTypeBuilder<UserStatistic> builder)
        {
            
            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.AppName)
                .IsRequired()
                .HasMaxLength(100);
                
            builder
                .Property(x => x.OperationName)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}