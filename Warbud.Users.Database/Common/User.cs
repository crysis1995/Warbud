using System;
using HotChocolate.AspNetCore.Authorization;

namespace Warbud.Users.Database.Common
{
    public abstract class User : AuditableEntity, IEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        [Authorize(Roles = new [] {Shared.Constants.Role.Name.Admin})]
        public Role Role { get; set; } = Role.Viewer;
    }
}