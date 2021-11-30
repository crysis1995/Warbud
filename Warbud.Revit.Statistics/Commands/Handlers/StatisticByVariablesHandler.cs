using System.Threading.Tasks;
using Warbud.Revit.Statistics.Entities;
using Warbud.Revit.Statistics.Interfaces;
using Warbud.Revit.Statistics.ValueObjects;
using Warbud.Shared.Abstraction.Commands;

namespace Warbud.Revit.Statistics.Commands.Handlers
{
    internal class StatisticByVariablesHandler : ICommandHandler<StatisticByVariables>
    {
        private readonly IStatisticRepository _repository;
        private readonly IStatisticFactory _factory;
        public StatisticByVariablesHandler(IStatisticRepository repository,
            IStatisticFactory factory)
        {
            _repository = repository;
            _factory = factory;
        }
        public async Task HandleAsync(StatisticByVariables command)
        {
            var (userName, userDomainName, computerName, appName, operationName, operationTimeMs, operationAmount) = command;
            var operation = new OperationData(appName, operationName, operationTimeMs, operationAmount);
            var user = new UserByVariables(userName, userDomainName, computerName);
            
            var statistic = _factory.Create(user, operation);
            await _repository.AddAsync(statistic);
        }
    }
}