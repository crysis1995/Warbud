using System;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Warbud.Users.Database.Common;
using Warbud.Users.Database.Models;
using Warbud.Users.GqlControllers;
using Warbud.Users.Infrastructure.Data;
using Warbud.Users.Types.Inputs;
using Xunit;

namespace Warbud.Tests.Users.Api.GqlControllers
{
    public class ExternalUserMutationsTest
    {
        public ExternalUser Owner { get; set; } = new() {Id = Guid.NewGuid(), FirstName = "InitName"};
        public ExternalUser Editor { get; set; } = new();

        [Theory]
        [InlineData(Role.Admin, true)]
        [InlineData(Role.BasicUser, true)]
        [InlineData(Role.Viewer, true)]
        [InlineData(Role.Admin, false)]
        [InlineData(Role.BasicUser, false)]
        [InlineData(Role.Viewer, false)]
        public async Task UpdateUser_ShouldUpdateUser_WhenUserIsAdminOrOwner(Role editorRole, bool owner)
        {
            //Arrange
            Editor.Id = owner ? Owner.Id : Guid.NewGuid();
            Editor.Role = editorRole;
            var input = new UpdateExternalUserInput(Editor.Id, "UpdatedName");
           
            
            var userRepository = new Mock<UserDbContext>();
            var usersMock = new Mock<DbSet<ExternalUser>>();
            usersMock.Setup(x => x.FindAsync(It.IsAny<ExternalUser>()))
                .ReturnsAsync(Owner);
            userRepository.Setup(x => x.ExternalUsers).Returns(usersMock.Object);
            
            var passwordHasher = new Mock<IPasswordHasher<ExternalUser>>();
            var userValidator = new Mock<IValidator<AddExternalUserInput>>();
            var appValidator = new Mock<IValidator<AddWarbudAppInput>>();
            var claimValidator = new Mock<IValidator<AddWarbudClaimInput>>();
            
            Mutation mutations = new(passwordHasher.Object, userValidator.Object, appValidator.Object,
                claimValidator.Object);

            //Act
            var result = await mutations.UpdateUserAsync(input, userRepository.Object);

            //Assert
            if (owner || editorRole == Role.Admin)
            {
                Assert.Equal(input.FirstName, Owner.FirstName);
                Assert.Equal(input.FirstName, result.User.FirstName);
            }
            else
            {
                Assert.NotEqual(input.FirstName, Owner.FirstName);
                Assert.Equal(input.FirstName, result.User.FirstName);
            }
        }
    }
}