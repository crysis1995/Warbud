using Microsoft.AspNetCore.Mvc;
using Warbud.Shared.Abstraction.Markers;

namespace Warbud.Users.GqlControllers
{
    public abstract class GqlBase : ControllerBase
    {
        protected ActionResult<TResult> OkOrNotFound<TResult>(TResult result)
            => result is null ? NotFound() : Ok(result);
    }
}