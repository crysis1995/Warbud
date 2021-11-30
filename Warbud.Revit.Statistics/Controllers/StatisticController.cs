using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Warbud.Revit.Statistics.Commands;
using Warbud.Revit.Statistics.DTO;
using Warbud.Revit.Statistics.Queries;
using Warbud.Shared.Abstraction.Commands;
using Warbud.Shared.Abstraction.Queries;

namespace Warbud.Revit.Statistics.Controllers
{
    [ApiController]
    [Route("api/revit/[controller]")]
    public class StatisticController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public StatisticController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatisticDto>>> Get()
        {
            var result = await _queryDispatcher.QueryAsync(new GetStatistics());
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] StatisticByVariables statistic)
        {
            if (statistic == null)
            {
                return BadRequest();
            }
            await _commandDispatcher.DispatchAsync(statistic);
            return Ok();
        }
        
        [HttpDelete]
        public async Task<ActionResult> Remove([FromBody] RemoveStatistic statistic)
        {
            await _commandDispatcher.DispatchAsync(statistic);
            return Ok();
        }
    }
}