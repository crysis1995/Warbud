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
    }
}