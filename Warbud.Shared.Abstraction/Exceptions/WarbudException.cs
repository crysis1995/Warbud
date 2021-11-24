using System;

namespace Warbud.Shared.Abstraction.Exceptions
{
    public abstract class WarbudException : Exception
    {
        protected WarbudException(string message) : base(message)
        {
        }
    }
}