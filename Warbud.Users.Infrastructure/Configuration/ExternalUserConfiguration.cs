using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warbud.Users.Database.Common;
using Warbud.Users.Database.Models;

namespace Warbud.Users.Infrastructure.Configuration
{
    public class ExternalUserConfiguration : IEntityTypeConfiguration<ExternalUser>
    {
        public void Configure(EntityTypeBuilder<ExternalUser> builder)
        {
            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100);
                
            builder
                .Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(x => x.Role)
                .IsRequired();

            builder.HasData(SeedUsers());
        }

        private static IEnumerable<ExternalUser> SeedUsers()
        {
            return new List<ExternalUser>()
            {
                new ExternalUser()
                {
                    Id = new Guid("F7260FE6-9B7D-4447-8D5D-4D8E5F9AA196"),
                    FirstName = "Mietek",
                    LastName = "Kowalski",
                    Role = Role.BasicUser
                },
            };
        }
    }
}