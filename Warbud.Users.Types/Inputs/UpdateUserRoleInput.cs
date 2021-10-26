using System;
using Warbud.Users.Database.Common;

namespace Warbud.Users.Types.Inputs
{
    /// <summary>
    /// Input for admin usage
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Role"></param>
    public record UpdateUserRoleInput(Guid Id, Role Role);
}