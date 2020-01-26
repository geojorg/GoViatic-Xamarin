using GoViatic.Common.Models;
using System.Threading.Tasks;

namespace GoViatic.Common.Services
{
    public interface IApiService
    {
        Task<Response<TravelerResponse>> GetTravelerByEmail(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            string email);

        Task<Response<TokenResponse>> GetTokenAsync(
            string urlBase,
            string servicePrefix,
            string controller,
            TokenRequest request);

        Task<bool> CheckConnection();
    }
}

