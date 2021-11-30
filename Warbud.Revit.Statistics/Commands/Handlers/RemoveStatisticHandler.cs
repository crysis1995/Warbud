using System.Threading.Tasks;
using Warbud.Revit.Statistics.Exceptions;
using Warbud.Revit.Statistics.Interfaces;
using Warbud.Shared.Abstraction.Commands;

namespace Warbud.Revit.Statistics.Commands.Handlers
{
    internal class RemoveStatisticHandler : ICommandHandler<RemoveStatistic>
    {
        
        private readonly IStatisticRepository _repository;
        public RemoveStatisticHandler(IStatisticRepository repository)
        {
            _repository = repository;
        }
        public async Task HandleAsync(RemoveStatistic command)
        {
            var statistic = await _repository.GetAsync(command.Id);
            if (statistic is null)
            {
                throw new StatisticNotFoundException();
            }
            await _repository.RemoveAsync(statistic);
        }
    }
}