using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Warbud.Shared.DTO;

namespace Warbud.Revit.Update.Controllers
{
    [Route("revit/[action]")]
    [ApiController]
    public class RevitController : ControllerBase
    {

        private static ProgramVersionDto _programVersionDto;

        public RevitController(ProgramVersionDto programVersionDto)
        {
            _programVersionDto = programVersionDto;
        }

        [HttpGet]
        public IActionResult Version()
        {
            var result = JsonSerializer.Serialize(_programVersionDto);
            return Ok(result);
        }
        
        [HttpPost]
        public IActionResult UpdateVersions([FromBody] ProgramVersionDto programVersionDto)
        {
            //TODO update in file 
            _programVersionDto = programVersionDto;
            return Ok();
        }
    }
}