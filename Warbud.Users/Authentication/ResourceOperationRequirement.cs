using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Warbud.Users.Constants;
using Warbud.Users.Database.Models;

namespace Warbud.Users.Authentication
{
    public enum ResourceOperation
    {
        Create = 0,
        Read = 1,
        Update = 2,
        Delete = 3,
    }
    
    public class ResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperation Operation { get; }
        public ResourceOperationRequirement(ResourceOperation operation)
        {
            Operation = operation;
        }
    }
    
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, ExternalUser>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement,
            ExternalUser user)
        {
            if (requirement.Operation == ResourceOperation.Read)
            {
                context.Succeed(requirement);
            }

            var userId = Guid.Parse(context.User.FindFirst(x => x.Type == Claims.ClaimsNames.Id).Value);
            if (user.Id == userId)
            {
                context.Succeed(requirement);
            }
            
            return Task.CompletedTask;
        }
    }
}