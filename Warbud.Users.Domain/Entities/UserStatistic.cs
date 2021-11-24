#nullable enable
using System;
using Warbud.Shared.Interfaces;

namespace Warbud.Users.Domain.Entities
{
    public class UserStatistic : IEntity
    {
        private UserStatistic()
        {
            
        }
        
        public UserStatistic(int userId, string appName, string operationName)
        {
            UserId = userId;
            AppName = appName;
            OperationName = operationName;
            DateTime = DateTime.Now;
        }
        
        public UserStatistic(string userName, string userDomainName, string computerName, string appName, string operationName, long operationTimeMs, int operationAmount)
        {
            UserName = userName;
            UserDomainName = userDomainName;
            ComputerName = computerName;
            AppName = appName;
            OperationName = operationName;
            DateTime = DateTime.Now;
            OperationTimeMs = operationTimeMs;
            OperationAmount = operationAmount;
        }

        public int Id { get; private  set;}
        public int? UserId { get; private set; }
        
        public long? OperationTimeMs { get; private set; }
        
        public int? OperationAmount { get; private set; }
        
        public string? UserName { get; private set;}
        public string? UserDomainName { get; private set;}
        public string? ComputerName { get; private set;}
        public string AppName { get; private  set; }
        public DateTime DateTime { get; private set; }
        public string OperationName { get; private set; }
    }
}