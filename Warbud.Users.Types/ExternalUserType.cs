using System.Linq;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using HotChocolate;
using HotChocolate.Types;
using Warbud.Users.Database.Models;

namespace Warbud.Users.Types
{
    public class ExternalUserType : ObjectType<ExternalUser>
    {
        protected override void Configure(IObjectTypeDescriptor<ExternalUser> descriptor)
        {
            descriptor.Description("Table od Warbud external users");

            descriptor.Field(x => x.Role)
                .Description("Role for user authorisation");

            descriptor.Field(x => x.Password).Ignore();
            descriptor.Field(x => x.ConfirmPassword).Ignore();
            descriptor.Field(x => x.PasswordHash).Ignore();
            
        }
    }
}