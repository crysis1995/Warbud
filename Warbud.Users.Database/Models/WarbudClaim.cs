using System;

namespace Warbud.Users.Database.Models
{
    public class WarbudClaim
    {
        private WarbudClaim(){}

        public WarbudClaim(Guid userId, int appId, int projectId, string name)
        {
            UserId = userId;
            AppId = appId;
            ProjectId = projectId;
            Name = name;
        }

        public Guid UserId { get; private set; }
        public int AppId { get;  private set;}
        public int ProjectId { get;  private set;}
        public string Name { get; set; }

#pragma warning disable 8632
        public override bool Equals(object? obj)
#pragma warning restore 8632
        {
            return obj is WarbudClaim claim && Equals(claim);
        }

        protected bool Equals(WarbudClaim other)
        {
            return UserId.Equals(other.UserId) && AppId == other.AppId && ProjectId == other.ProjectId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserId, AppId, ProjectId);
        }
    }
}