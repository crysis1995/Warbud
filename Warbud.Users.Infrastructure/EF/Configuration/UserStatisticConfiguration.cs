// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using Warbud.Users.Domain.Entities;
//
// namespace Warbud.Users.Infrastructure.EF.Configuration
// {
//     public class UserStatisticConfiguration : IEntityTypeConfiguration<UserStatistic>
//     {
// //TODO : Add configuration for UserStatistic
//         public void Configure(EntityTypeBuilder<UserStatistic> builder)
//         {
//             
//             builder
//                 .Property(x => x.Id)
//                 .ValueGeneratedOnAdd();
//
//             builder
//                 .Property(x => x.AppName)
//                 .IsRequired()
//                 .HasMaxLength(100);
//                 
//             builder
//                 .Property(x => x.OperationName)
//                 .IsRequired()
//                 .HasMaxLength(100);
//         }
//     }
// }