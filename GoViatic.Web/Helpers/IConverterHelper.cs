using GoViatic.Common.Models;
using GoViatic.Web.Data.Entities;
using GoViatic.Web.Models;
using System.Threading.Tasks;

namespace GoViatic.Web.Helpers
{
    public interface IConverterHelper
    {
        Task<Viatic> ToViaticAsync(ViaticViewModel model, string path, bool isNew);
        ViaticViewModel ToViaticViewModel(Viatic viatic);
        Task<Trip> ToTripAsync(TripViewModel model, bool isNew);
        TripViewModel ToTripViewModel(Trip trip);
        TripResponse ToTripResponse(Trip trip);
        TravelerResponse ToTravelerResponse(Traveler traveler);
    }
}