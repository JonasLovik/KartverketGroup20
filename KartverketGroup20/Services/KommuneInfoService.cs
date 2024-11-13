using KartverketGroup20.APIModels;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace KartverketGroup20.Services
{
//    public class KommuneInfoService : IKommuneInfoService
//    {
//        private readonly HttpClient _httpClient;
//        private readonly ILogger<KommuneInfoService> _logger;
//        private readonly ApiSettings _apiSettings;

//        public KommuneInfoService(HttpClient httpClient, ILogger<KommuneInfoService> logger, IOptions<ApiSettings> apiSettings)
//        {
//            _httpClient = httpClient;
//            _logger = logger;
//            _apiSettings = apiSettings.Value;
//        }

//        public async Task<KommuneInfo> GetKommuneInfoAsync(string kommuneNr)
//        {
//            try
//            {
//                var response = await _httpClient.GetAsync($"{_apiSettings.KommuneInfoApiBaseUrl}/kommuner/{kommuneNr}");
//                response.EnsureSuccessStatusCode();

//                var json = await response.Content.ReadAsStringAsync();
//                _logger.LogInformation($"KommuneInfo Response: {json}");
//                var kommuneInfo = JsonSerializer.Deserialize<KommuneInfo>(json);
//                return kommuneInfo;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"Error fetching KommunInfo for {kommuneNr}: {ex.Message}");
//                return null;

//            }
//        }
//    }
}
