using Microsoft.AspNetCore.Mvc;

namespace Warbud.Users.GqlControllers
{
    public class RestQueryBase: ControllerBase
    {

        protected ActionResult<TResult> OkOrNotFound<TResult>(TResult result)
            => result is null ? NotFound() : Ok(result);    }
}