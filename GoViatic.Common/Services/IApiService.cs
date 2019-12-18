using GoViatic.Common.Models;
using System.Threading.Tasks;

namespace GoViatic.Common.Services
{
    public interface IApiService
    {
        Task<Response> GetTravelerByEmail(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            string email);

        Task<Response> GetTokenAsync(
            string urlBase,
            string servicePrefix,
            string controller,
            TokenRequest request);
    }
}

