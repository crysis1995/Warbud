using Warbud.Users.Exceptions;

namespace Warbud.Users.GqlControllers
{
    public abstract class GqlQueryBase
    {
        protected TResult OkOrNotFoundGql<TResult>(TResult result)
            => result is null ? throw new NotFountException() : result;
    }
}