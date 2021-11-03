﻿using Warbud.Users.Database.Common;

namespace Warbud.Users.Database.Models
{
    public class WarbudApp : AuditableEntity, IEntity
    {
        public int Id { get; set; }
        public string AppName { get; set; }
        public string ModuleName { get; set; }
    }
}