using System;

namespace Warbud.Users.Database.Common
{
    public abstract class User : AuditableEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; } = Role.Viewer;
    }
}