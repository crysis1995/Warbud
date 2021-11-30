using Warbud.Revit.Statistics.DTO;
using Warbud.Revit.Statistics.Entities;
using Warbud.Revit.Statistics.ValueObjects;

namespace Warbud.Revit.Statistics.Queries
{
    internal static class Extensions
    {
        public static StatisticDto AsDto(this Statistic model)
        {
            var (userName, userDomainName, computerName) = model.UserByVariables;
            var user = $"{userName} | {userDomainName} | {computerName}";

            var (appName, operationName, timeMs, amount) = model.OperationData;
            return new StatisticDto(model.Id, user, appName, operationName, timeMs, amount, model.DateTime);
        }

    }
}