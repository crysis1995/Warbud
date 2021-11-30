using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warbud.Revit.Statistics.Entities;
using Warbud.Revit.Statistics.ValueObjects;

namespace Warbud.Revit.Statistics.Infrastructure.Configurations
{
    internal sealed class StatisticConfiguration : IEntityTypeConfiguration<Statistic>
    {
        public void Configure(EntityTypeBuilder<Statistic> builder)
        {
            builder.HasKey(pl => pl.Id);
            builder.Property(pl => pl.Id).ValueGeneratedOnAdd();
            
            builder.OwnsOne(x => x.OperationData)
                .Property(x => x.AppName)
                .HasColumnName("AppName")
                .HasColumnType("varchar(100)");

            builder.OwnsOne(x => x.OperationData)
                .Property(x => x.OperationName)
                .HasColumnName("OperationName");
            
            builder.OwnsOne(x => x.OperationData)
                .Property(x => x.OperationAmount)
                .HasColumnName("OperationAmount")
                .HasColumnType("int");

            builder.OwnsOne(x => x.OperationData)
                .Property(x => x.OperationTimeMs)
                .HasColumnName("OperationTimeMs")
                .HasColumnType("bigint");
            
            builder.OwnsOne(x => x.UserByVariables)
                .Property(x => x.UserName)
                .HasColumnName("UserName")
                .HasColumnType("varchar(50)");
            
            builder.OwnsOne(x => x.UserByVariables)
                .Property(x => x.UserDomainName)
                .HasColumnName("DomainName")
                .HasColumnType("varchar(50)");
            
            builder.OwnsOne(x => x.UserByVariables)
                .Property(x => x.ComputerName)
                .HasColumnName("ComputerName")
                .HasColumnType("varchar(50)");
            
            builder.OwnsOne(x => x.UserByVariables);
            
            builder.ToTable("Statistics");
        }
    }
}