using System.Linq;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using HotChocolate;
using HotChocolate.Types;
using Warbud.Users.Database.Models;

namespace Warbud.Users.Types
{
    public class InternalUserType : ObjectType<InternalUser>
    {
        protected override void Configure(IObjectTypeDescriptor<InternalUser> descriptor)
        {
            descriptor.Description("Table od Warbud internal users");

            descriptor.Field(x => x.Role)
                .Description("Role for user authorisation");
        }
    }
}