using MassTransit;
using Message;

namespace Pickpoint.RMQ.Publisher
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
            services.AddControllers();

            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((context, cfg) =>
                {
                    var rabbitConfig = this.Configuration.GetSection("Rabbit");

                    cfg.Host($"amqp://{rabbitConfig["HostName"]}:{rabbitConfig["Port"]}", y =>
                    {
                        y.Username(rabbitConfig["UserName"]);
                        y.Password(rabbitConfig["Password"]);
                    });

                    cfg.Message<SendMessage>(x =>
                    {
                        x.SetEntityName("Publisher");
                    });
                });
            });
            services.AddMassTransitHostedService();
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
        }
    }

}

