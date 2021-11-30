using System.Collections.Generic;
using Warbud.Revit.Statistics.DTO;
using Warbud.Shared.Abstraction.Queries;

namespace Warbud.Revit.Statistics.Queries
{
    public record GetStatistics : IQuery<IEnumerable<StatisticDto>>;
}