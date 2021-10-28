using System;

namespace Warbud.Users.Types.Inputs
{
    
    public record GetWarbudClaimInput(Guid UserId, int AppId, int ProjectId);
    public record AddWarbudClaimInput(Guid UserId, int AppId, int ProjectId, string Name);

    public record UpdateWarbudClaimInput(Guid UserId, int AppId, int ProjectId, string Name);

    public record DeleteWarbudClaimInput(Guid UserId, int AppId, int ProjectId);
}