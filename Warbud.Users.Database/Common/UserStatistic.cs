using System;
using System.Collections.Generic;
using Warbud.Shared.Interfaces;

namespace Warbud.Users.Database.Common
{
    public class UserStatistic : IEntity
    {
        public UserStatistic(int userId, string appName, string operationName)
        {
            UserId = userId;
            AppName = appName;
            OperationName = operationName;
            DateTime = DateTime.Now;
        }

        public int Id { get; private  set;}
        public int UserId { get; private set; }
        public string AppName { get; private  set; }
        public DateTime DateTime { get; private set; }
        public string OperationName { get; private set; }
    }
}