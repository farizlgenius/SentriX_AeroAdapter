using AeroAdapter.Application.DTOs;
using AeroAdapter.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AeroAdapter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScpController(IScpService service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> SendASCIICommandAsync([FromBody] ASCIICommandDto Command)
        {
            var res = await service.SendASCIICommandAsync(Command);
            return Ok(res);
        }
    }
}
