using System;

namespace Warbud.Users.Database.Common
{
    public abstract class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; } = Role.Viewer;
        
        public DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public int LastModifiedBy { get; set; }
    }
}