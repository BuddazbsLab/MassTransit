using MassTransit;
using Message;
using Microsoft.AspNetCore.Mvc;

namespace Pickpoint.RMQ.Publisher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase

    {
        private readonly IPublishEndpoint publishEndpoint;

        public MessageController(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint; 
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SendMessage message)
        {
            await publishEndpoint.Publish(message);
            

            return Ok();
        }
    }
}
