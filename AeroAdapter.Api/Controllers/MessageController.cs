using AeroAdapter.Application.DTOs;
using AeroAdapter.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AeroAdapter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController(IMessagePublisher publisher) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> PostMessageAsync([FromBody] MessageDto message )
        {
            await publisher.PublishAsync<object>(message.Exchange,message.Key,message.Data);
            return Ok();
        }
    }
}
