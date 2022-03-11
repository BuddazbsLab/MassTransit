using Pickpoint.RMQ.Publisher;
using NLog;
using NLog.Web;

{
    var logger = NLog.Web.NLogBuilder.ConfigureNLog("Nlog.config").GetCurrentClassLogger();
    try
    {
        logger.Info("Application as Started. Press Ctrl+C to shut down");
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
            logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            logging.AddConsole();
        })
        .UseNLog();

