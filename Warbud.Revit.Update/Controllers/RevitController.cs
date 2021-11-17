using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Warbud.Revit.Update.Models;

namespace Warbud.Revit.Update.Controllers
{
    [Route("revit/[action]")]
    [ApiController]
    public class RevitController : ControllerBase
    {

        private static ProgramVersion _programVersion;

        public RevitController(ProgramVersion programVersion)
        {
            _programVersion = programVersion;
        }

        [HttpGet]
        public IActionResult Version()
        {
            var result = JsonSerializer.Serialize(_programVersion);
            return Ok(result);
        }
        
        [HttpPost]
        public IActionResult UpdateVersions([FromBody] ProgramVersion programVersion)
        {
            //TODO update in file 
            _programVersion = programVersion;
            return Ok();
        }
    }
}