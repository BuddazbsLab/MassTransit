using MassTransit;
using Message;
using NLog;
using NLog.Web;

namespace Pickpoint.RMQ.Consumer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMassTransit(x =>
            {
                x.AddConsumer<MessageEventConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    var rabbitConfig = this.Configuration.GetSection("Rabbit");

                    cfg.UseJsonSerializer();

                    cfg.Host($"amqp://{rabbitConfig["HostName"]}:{rabbitConfig["Port"]}", y =>
                    {
                        y.Username(rabbitConfig["UserName"]);
                        y.Password(rabbitConfig["Password"]);
                          
                    });

                    cfg.Message<SendMessage>(x =>
                    {
                        x.SetEntityName("Consumer");
                        
                    });
                    cfg.ReceiveEndpoint("message-event", e =>
                    {
                        e.ConfigureConsumer<MessageEventConsumer>(context);
                        e.Bind("Publisher");
                    });
                });
            });

            services.AddMassTransitHostedService();

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }
    }
}

