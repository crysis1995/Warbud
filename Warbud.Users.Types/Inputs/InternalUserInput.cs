using System;
using Warbud.Users.Database.Common;

namespace Warbud.Users.Types.Inputs
{
    public record AddInternalUserInput(string FirstName, string LastName, Role Role = Role.BasicUser);

#nullable enable
    public record UpdateInternalUserInput(Guid Id, string? FirstName = null, string? LastName = null);
#nullable disable
}