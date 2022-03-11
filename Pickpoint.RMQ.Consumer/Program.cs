using Pickpoint.RMQ.Consumer;
using NLog;
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
        LogManager.Shutdown();
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

