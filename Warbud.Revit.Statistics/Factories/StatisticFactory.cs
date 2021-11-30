using Warbud.Revit.Statistics.Entities;
using Warbud.Revit.Statistics.Interfaces;
using Warbud.Revit.Statistics.ValueObjects;

namespace Warbud.Revit.Statistics.Factories
{
    internal class StatisticFactory : IStatisticFactory
    {
        public Statistic Create(UserByVariables userByVariables, OperationData operationData)
            => new (userByVariables, operationData);
    }
}