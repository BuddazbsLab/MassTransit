using MassTransit;
using Message;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Pickpoint.RMQ.Publisher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase

    {
        private readonly IPublishEndpoint publishEndpoint;
        private readonly ILogger<MessageController> _logger;

        public MessageController(ILogger<MessageController> logger)
        {
            _logger = logger;
        }

        public MessageController(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
           
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SendMessage message)
        {
            await publishEndpoint.Publish(message);
            _logger.LogInformation($"Message Controller Invoke");
            _logger.LogInformation(message.Message);

            return Ok();
        }
    }
}
