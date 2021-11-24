using HotChocolate.Types;
using Warbud.Users.Domain.Entities;

namespace Warbud.Users.Application
{
    public class ExternalUserType : ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor.Description("Table od Warbud external users");

            descriptor.Field(x => x.Role)
                .Description("Role for user authorisation");
            descriptor.Field(x => x.Password).Ignore();
        }
    }
}