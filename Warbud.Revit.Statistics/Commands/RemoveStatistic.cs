using Warbud.Shared.Abstraction.Commands;

namespace Warbud.Revit.Statistics.Commands
{
    public record RemoveStatistic(int Id) : ICommand;
}