using PopulationChecker.WorkerService.Services;

namespace PopulationChecker.WorkerService;

public class Worker : BackgroundService
{
    //private readonly ILogger<Worker> _logger;
    //private readonly IServiceProvider _services;

    //public Worker(ILogger<Worker> logger, IServiceProvider services)
    //{
    //    _logger = logger;
    //    _services = services;
    //}

    //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    //{
    //    var runTime = TimeSpan.Parse("00:00");

    //    while (!stoppingToken.IsCancellationRequested)
    //    {
    //        if (DateTime.Now.TimeOfDay >= runTime && DateTime.Now.TimeOfDay < runTime.Add(TimeSpan.FromMinutes(1)))
    //        {
    //            using (var scope = _services.CreateScope())
    //            {
    //                var service = scope.ServiceProvider.GetRequiredService<PopulationService>();
    //                await service.FetchAndSaveCitizenInfoAsync();
    //            }
    //        }

    //        await Task.Delay(60000, stoppingToken); // Her dakikada bir kontrol et
    //    }
    //}

    private readonly ILogger<Worker> _logger;
    private readonly PopulationService _populationService;

    public Worker(ILogger<Worker> logger, PopulationService populationService)
    {
        _logger = logger;
        _populationService = populationService;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Veri çekme iþlemi baþlýyor...");

            // Test veri kullanarak metodu çaðýrýyoruz
            await _populationService.FetchAndSaveCitizenInfoAsync();

            _logger.LogInformation("Veri çekme iþlemi tamamlandý.");

            // Bekleme süresi (örneðin, her gece saat 00:00'da çalýþacak)
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);  // Her gün bir kez çalýþacak
        }
    }
}

