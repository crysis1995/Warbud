using System;
using Warbud.Users.Database.Common;

namespace Warbud.Users.Types.Inputs
{
    public record UpdateUserRoleInput(Guid Id, Role Role);
}