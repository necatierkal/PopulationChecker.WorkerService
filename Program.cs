using PopulationChecker.WorkerService.Data;
using PopulationChecker.WorkerService.Services;
using Serilog;
using Microsoft.EntityFrameworkCore;
using PopulationChecker.WorkerService;
using Microsoft.Extensions.Hosting.WindowsServices;




Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logs/population_log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .UseSerilog()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection")));

        services.AddTransient<PopulationService>();
        services.AddHostedService<Worker>();
    })
    .Build();



host.Run();


