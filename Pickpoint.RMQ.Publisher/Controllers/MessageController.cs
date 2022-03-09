using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Pickpoint.RMQ.Publisher.Model;

namespace Pickpoint.RMQ.Publisher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase

    {
        readonly IPublishEndpoint _publishEndpoint;

        public MessageController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SendMessage message)
        {
            await _publishEndpoint.Publish<SendMessage>(new
            {
                Message = message
            });

            return Ok();
        }
    }
}
