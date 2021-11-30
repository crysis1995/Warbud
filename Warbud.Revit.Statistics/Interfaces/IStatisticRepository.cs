using System.Threading.Tasks;
using Warbud.Revit.Statistics.Entities;

namespace Warbud.Revit.Statistics.Interfaces
{
    internal interface IStatisticRepository
    {
        Task<Statistic> GetAsync(int id);
        Task AddAsync(Statistic statistic);
        Task RemoveAsync(Statistic statistic);
    }
}