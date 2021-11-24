// using System;
// using System.Threading.Tasks;
// using FluentValidation;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using Moq;
// using Warbud.Users.GqlControllers;
// using Warbud.Users.Infrastructure.Data;
// using Warbud.Users.Application.Commands.User;
// using Warbud.Users.Application.Commands.WarbudApp;
// using Warbud.Users.Application.Commands.WarbudClaim;
// using Warbud.Users.Domain.Constants;
// using Warbud.Users.Domain.Entities;
// using Xunit;
//
// namespace Warbud.Tests.Users.Api.GqlControllers
// {
//     public class ExternalUserMutationsTest
//     {
//         public User Owner { get; set; } = new() {Id = Guid.NewGuid(), FirstName = "InitName"};
//         public User Editor { get; set; } = new();
//
//         [Theory]
//         [InlineData(Role.Admin, true)]
//         [InlineData(Role.BasicUser, true)]
//         [InlineData(Role.Viewer, true)]
//         [InlineData(Role.Admin, false)]
//         [InlineData(Role.BasicUser, false)]
//         [InlineData(Role.Viewer, false)]
//         public async Task UpdateUser_ShouldUpdateUser_WhenUserIsAdminOrOwner(Role editorRole, bool owner)
//         {
//             //Arrange
//             Editor.Id = owner ? Owner.Id : Guid.NewGuid();
//             Editor.Role = editorRole;
//             var input = new UpdateUser(Editor.Id, "UpdatedName");
//            
//             
//             var userRepository = new Mock<UserDbContext>();
//             var usersMock = new Mock<DbSet<User>>();
//             usersMock.Setup(x => x.FindAsync(It.IsAny<User>()))
//                 .ReturnsAsync(Owner);
//             userRepository.Setup(x => x.ExternalUsers).Returns(usersMock.Object);
//             
//             var passwordHasher = new Mock<IPasswordHasher<User>>();
//             var userValidator = new Mock<IValidator<AddUser>>();
//             var appValidator = new Mock<IValidator<AddWarbudApp>>();
//             var claimValidator = new Mock<IValidator<AddWarbudClaim>>();
//             
//             Mutation mutations = new(passwordHasher.Object, userValidator.Object, appValidator.Object,
//                 claimValidator.Object);
//
//             //Act
//             var result = await mutations.UpdateUserAsync(input, userRepository.Object);
//
//             //Assert
//             if (owner || editorRole == Role.Admin)
//             {
//                 Assert.Equal(input.FirstName, Owner.FirstName);
//                 Assert.Equal(input.FirstName, result.User.FirstName);
//             }
//             else
//             {
//                 Assert.NotEqual(input.FirstName, Owner.FirstName);
//                 Assert.Equal(input.FirstName, result.User.FirstName);
//             }
//         }
//     }
// }