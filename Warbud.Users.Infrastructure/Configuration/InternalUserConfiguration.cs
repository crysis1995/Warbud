using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warbud.Users.Database.Models;
using System.Collections.Generic;
using Warbud.Users.Database.Common;

namespace Warbud.Users.Infrastructure.Configuration
{
    public class InternalUserConfiguration : IEntityTypeConfiguration<InternalUser>
    {
            public void Configure(EntityTypeBuilder<InternalUser> builder)
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

            private static IEnumerable<InternalUser> SeedUsers()
            {
                return new List<InternalUser>()
                {
                    new InternalUser()
                    {
                        Id = new Guid("D27C7FED-8942-4158-A006-0850A6F16F57"),
                        FirstName = "Adrian",
                        LastName = "Franczak",
                        Role = Role.Admin
                    },
                    new InternalUser()
                    {
                        Id = new Guid("99185134-57E5-4329-AF88-62D3624490A4"),
                        FirstName = "Krzysztof",
                        LastName = "Kaczor",
                        Role = Role.Admin
                    },
                };
            }
        
    }
}