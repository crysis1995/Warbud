using Warbud.Users.Database.Common;

namespace Warbud.Users.Database.Models
{
    public class ExternalUser : User, IEntity
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}