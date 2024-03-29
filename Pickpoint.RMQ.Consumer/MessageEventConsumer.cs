﻿using MassTransit;
using Message;

namespace Pickpoint.RMQ.Consumer
{
    class MessageEventConsumer : IConsumer<SendMessage>
    {
        private readonly ILogger<MessageEventConsumer> logger;

        public MessageEventConsumer(ILogger<MessageEventConsumer> logger)
        {
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<SendMessage> context)
        {
            
            logger.LogInformation($"Get a new message: {context.Message.Message}");
        }
    }
}
