using System;
using System.Threading.Tasks;
using Moq;
using Warbud.Users.Database.Common;
using Warbud.Users.Database.Models;
using Warbud.Users.Infrastructure.Data;
using Warbud.Users.Types.Inputs;
using Xunit;

namespace Warbud.Tests.Users.Api.GqlControllers
{
    public class ExternalUserMutationsTest
    {
        public ExternalUser Owner { get; set; } = new(){Id = Guid.NewGuid(), FirstName = "InitName"};
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
            Editor.Id = owner ? Owner.Id : Guid.NewGuid();
            Editor.Role = editorRole;
            var input = new UpdateExternalUserInput(Editor.Id, "UpdatedName");
            
            // var userRepository = new Mock<UserDbContext>();
            // userRepository.Setup(x => x.ExternalUsers).ReturnsAsync(new[] {Owner, Editor});
            // userRepository.Setup(x => x.UpdateUserAsync(It.IsAny<User>())).ReturnsAsync(user);
            //
            // var result = await userController.UpdateUser(input);
            //
            // Assert.NotNull(result);
            // Assert.Equal(user.Id, result.Id);
            // Assert.Equal(user.Role, result.Role);
            //  
            // Warbud.Users.GqlControllers.Mutation mutations = new();
            // await mutations.UpdateUserAsync(input, new UserDbContext());
        }
    }
}