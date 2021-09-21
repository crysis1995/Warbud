using System;
using Warbud.Users.Database.Common;

namespace Warbud.Users.Types.Inputs
{
    public record AddExternalUserInput(string FirstName, string LastName, Role Role = Role.BasicUser);

#nullable enable
    public record UpdateExternalUserInput(Guid Id, string? FirstName = null, string? LastName = null);
#nullable disable
}