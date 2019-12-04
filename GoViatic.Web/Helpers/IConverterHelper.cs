using GoViatic.Web.Data.Entities;
using GoViatic.Web.Models;
using System.Threading.Tasks;

namespace GoViatic.Web.Helpers
{
    public interface IConverterHelper
    {
        Task<Viatic> ToViaticAsync(ViaticViewModel model, string path);
        Task<Trip> ToTripAsync(TripViewModel model, bool isNew);


    }
}