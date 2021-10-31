using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Warbud.Users.Constants;

namespace Warbud.Users.Authentication
{
    public class DoILikeYouRequirements : IAuthorizationRequirement
    {
        public bool DoILikeYou { get; }
        public DoILikeYouRequirements(bool doILikeYou)
        {
            DoILikeYou = doILikeYou;
        }
    }
    
    public class DoILikeYouRequirementsHandler : AuthorizationHandler<DoILikeYouRequirements>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DoILikeYouRequirements requirement)
        {
            var doILikeYou =bool.Parse(context.User?.FindFirst(x => x.Type == Policy.PolicyNames.DoILikeYou)?.Value);
            if (doILikeYou == requirement.DoILikeYou)
            {
                context.Succeed(requirement);
            }
            
            return Task.CompletedTask;
        }
    }
}