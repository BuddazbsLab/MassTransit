using MassTransit;

namespace Pickpoint.RMQ.Consumer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMassTransit(x =>
            {
                x.AddConsumer<EventConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    var rabbitConfig = this.Configuration.GetSection("Rabbit");

                    cfg.Host($"{rabbitConfig["HostName"]}", y =>
                    {
                        y.Username(rabbitConfig["UserName"]);
                        y.Password(rabbitConfig["Password"]);
                    });

                    cfg.ReceiveEndpoint("event-consumer", e =>
                    {
                        e.ConfigureConsumer<EventConsumer>(context);                        
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

