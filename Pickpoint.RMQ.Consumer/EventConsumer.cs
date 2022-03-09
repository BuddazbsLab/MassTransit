using MassTransit;
using Pickpoint.RMQ.Consumer.Model;

namespace Pickpoint.RMQ.Consumer
{
    class EventConsumer : IConsumer<SendMessage>
    {
        ILogger<EventConsumer> _logger;

        public EventConsumer(ILogger<EventConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<SendMessage> context)
        {
            await Console.Out.WriteLineAsync(context.Message.Message);
            _logger.LogInformation($"Get a new message: {context.Message.Message}");
        }
    }
}
