using System;

namespace Warbud.Users.Types.Inputs
{
    public record AddWarbudClaimInput(Guid UserId, int AppId, int ProjectId, string Name);

    public record UpdateWarbudClaimInput(string Name);

    public record RemoveWarbudClaimInput(Guid UserId, int AppId, int ProjectId);
}