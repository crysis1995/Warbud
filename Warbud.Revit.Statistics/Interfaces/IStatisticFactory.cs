using Warbud.Revit.Statistics.Entities;
using Warbud.Revit.Statistics.ValueObjects;

namespace Warbud.Revit.Statistics.Interfaces
{
    internal interface IStatisticFactory
    {
        Statistic Create(UserByVariables userByVariables, OperationData operationData);
    }
}