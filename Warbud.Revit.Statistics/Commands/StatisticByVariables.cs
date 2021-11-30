using Warbud.Shared.Abstraction.Commands;

namespace Warbud.Revit.Statistics.Commands
{
    public record StatisticByVariables(string UserName,
        string UserDomainName,
        string ComputerName,
        string AppName,
        string OperationName,
        long OperationTimeMs,
        int OperationAmount): ICommand;
}