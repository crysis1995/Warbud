using System;
using Warbud.Users.Database.Common;
using Xunit;
using Warbud.Users.Database.Models;

namespace Warbud.Tests.Users.Database.Models
{
    public class UsersTest
    {
        //create test user

        [Fact]
        public void CreateUser()
        {
            var user = new ExternalUser()
            {
                Id = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1),
                FirstName = "Name",
                Email = "NameSurname@gmail.com",
                LastName = "Surname",
                Password = "Password",
                Role = Role.Viewer
            };

            Assert.Equal(user.Id, new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1));
        }
    }
}