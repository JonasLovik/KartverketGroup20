using KartverketGroup20.APIModels;

namespace KartverketGroup20.Services
{
    public interface IKommuneInfoService
    {
        Task<KommuneInfo> GetKommuneInfoAsync(string kommuneNr);
    }
}
