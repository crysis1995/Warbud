using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warbud.Users.Database.Models;

namespace Warbud.Users.Infrastructure.Configuration
{
    public class ExternalUserConfiguration : IEntityTypeConfiguration<ExternalUser>
    {

        public void Configure(EntityTypeBuilder<ExternalUser> builder)
        {
            builder
                .HasIndex(x => x.Email)
                .IsUnique();
            
            builder.Ignore(x => x.Password);
            builder.Ignore(x => x.ConfirmPassword);
            
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
            
            builder
                .Property(x => x.PasswordHash)
                .IsRequired();

            builder
                .Property(x => x.Email)
                .IsRequired();
            
            //builder.HasData(SeedUsers());
        }

        private IEnumerable<ExternalUser> SeedUsers()
        {
            return new List<ExternalUser>()
            {
                new()
                {
                    Id = new Guid("F7260FE6-9B7D-4447-8D5D-4D8E5F9AA196"),
                    FirstName = "Mietek",
                    LastName = "Kowalski",
                    Email = "mietekkowalski@warbud.pl",
                    PasswordHash = "HasloNapotrzebyMigracji"
                }
            };
        }
    }
}