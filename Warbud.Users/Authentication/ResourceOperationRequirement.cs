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
            
            var role = context.User.FindFirst(x => x.Type == Claims.ClaimsNames.Role)?.Value;
            if (role == Claims.RoleValues.Admin)
            {
                context.Succeed(requirement);
            }

            switch (requirement.Operation)
            {
                case ResourceOperation.Delete or ResourceOperation.Create:
                    context.Fail();
                    break;
                case ResourceOperation.Read:
                    context.Succeed(requirement);
                    break;
            }

            var value = context.User.FindFirst(x => x.Type == Claims.ClaimsNames.Id)?.Value;
            if (string.IsNullOrWhiteSpace(value)) return Task.CompletedTask;
            var userId = Guid.Parse(value);
            if (user.Id == userId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}