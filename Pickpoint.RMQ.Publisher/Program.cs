using Pickpoint.RMQ.Publisher;
using NLog.Web;

{
    var logger = NLogBuilder.ConfigureNLog("Nlog.config").GetCurrentClassLogger();
    try
    {
        
        CreateHostBuilder(args).Build().Run();

    }
    catch (Exception ex)
    {
        logger.Error(ex, "Exception during execution.");
        throw;
    }
    finally
    {
        NLog.LogManager.Shutdown();
    }

    

}

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        })
        .ConfigureLogging(logging =>
        {
            logging.ClearProviders();
        })
        .UseNLog();

