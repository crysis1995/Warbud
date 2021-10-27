using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warbud.Users.Database.Models;

namespace Warbud.Users.Infrastructure.Configuration
{
    public class WarbudClaimConfiguration : IEntityTypeConfiguration<WarbudClaim>
    {

        public void Configure(EntityTypeBuilder<WarbudClaim> builder)
        {
            builder
                .HasKey(x => new
                {
                    x.UserId,
                    x.AppId,
                    x.ProjectId
                });

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}