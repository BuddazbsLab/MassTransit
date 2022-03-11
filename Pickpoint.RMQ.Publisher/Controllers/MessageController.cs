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

        [RunJob("0/30 * * * * ?")]
        public async Task EventPublishMessage()
        {
            await publishEndpoint.Publish(new SendMessage
            {
                Message = "Hello, it's work job", 
            });
            Console.Out.WriteLine("Send message: Hello, it's work job");
        }

    }
}
