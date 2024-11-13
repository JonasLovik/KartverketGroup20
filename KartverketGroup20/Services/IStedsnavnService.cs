using KartverketGroup20.APIModels;

namespace KartverketGroup20.Services
{
    public interface IStedsnavnService
    {
        Task<StedsnavnResponse> GetStedsnavnAsync(string search);
    }
}
