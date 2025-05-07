using PopulationChecker.WorkerService.Data;
using PopulationChecker.WorkerService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PopulationChecker.WorkerService.Services
{
    public class PopulationService
    {
        private readonly ILogger<PopulationService> _logger;
        private readonly IServiceProvider _services;

        public PopulationService(ILogger<PopulationService> logger, IServiceProvider services)
        {
            _logger = logger;
            _services = services;
        }

        public async Task FetchAndSaveCitizenInfoAsync()
        {
            try
            {
                var client = new PopulationServiceReference.KPSPublicSoapClient(
                    PopulationServiceReference.KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap);

                long tckn = 25588644316; // Gerçek bir TCKN yerine örnek numara
                string ad = "Necati";
                string soyad = "ERKAL";
                int dogumYili = 1989;

                var response = await client.TCKimlikNoDogrulaAsync(tckn, ad, soyad, dogumYili);

                if (response.Body.TCKimlikNoDogrulaResult)
                {
                    var citizen = new CitizenInfo
                    {
                        FullName = $"{ad} {soyad}",
                        NationalId = tckn.ToString(),
                        BirthDate = new DateTime(dogumYili, 1, 1)
                    };

                    using var scope = _services.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    db.Citizens.Add(citizen);
                    await db.SaveChangesAsync();

                    _logger.LogInformation($"[{DateTime.Now}] Doğrulama başarılı ve veri kaydedildi: {citizen.FullName}");
                }
                else
                {
                    _logger.LogWarning($"[{DateTime.Now}] Doğrulama başarısız: {ad} {soyad}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SOAP çağrısı veya kayıt sırasında hata oluştu.");
            }
        }

    }

}
