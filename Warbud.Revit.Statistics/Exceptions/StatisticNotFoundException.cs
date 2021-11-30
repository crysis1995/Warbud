using Warbud.Shared.Abstraction.Exceptions;

namespace Warbud.Revit.Statistics.Exceptions
{
    public class StatisticNotFoundException : WarbudException
    {
        public StatisticNotFoundException() : base("The requested element was not found.")
        {
        }
    }
}