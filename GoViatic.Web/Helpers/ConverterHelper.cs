using GoViatic.Web.Data;
using GoViatic.Web.Data.Entities;
using GoViatic.Web.Models;
using System.Threading.Tasks;

namespace GoViatic.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _context;

        public ConverterHelper(DataContext context)
        {
            _context = context;
        }
        public async Task<Trip> ToTripAsync(TripViewModel model, bool isNew)
        {
            return new Trip
            {
                Id = isNew ? 0 : model.Id,
                City = model.City,
                Date = model.Date.ToUniversalTime(),
                EndDate = model.EndDate.ToUniversalTime(),
                Bugdet = model.Bugdet,
                Traveler = await _context.Travelers.FindAsync(model.TravelerId),
                Viatics = model.Viatics
            };
        }

        public TripViewModel ToTripViewModel(Trip trip)
        {
            return new TripViewModel
            {
                City = trip.City,
                Date = trip.Date.ToUniversalTime(),
                EndDate = trip.EndDate.ToUniversalTime(),
                Bugdet = trip.Bugdet,
                Traveler = trip.Traveler,
                Viatics = trip.Viatics,
                Id = trip.Id,
                TravelerId = trip.Traveler.Id
            };
        }

        public async Task<Viatic> ToViaticAsync(ViaticViewModel model, string path, bool isNew)
        {
            var viatic = new Viatic
            {
                Id = isNew ? 0 : model.Id,
                Description = model.Description,
                ImageUrl = path,
                InvoiceDate = model.InvoiceDate,
                InvoiceAmmount = model.InvoiceAmmount,
                ViaticName = model.ViaticName,
                ViaticType = model.ViaticType,
                Traveler = await _context.Travelers.FindAsync(model.Traveler),
                Trip = await _context.Trips.FindAsync(model.TripId),
            };
            return viatic;
        }
        public ViaticViewModel ToViaticViewModel(Viatic viatic)
        {
            return new ViaticViewModel
            {
                Id = viatic.Id,
                Description = viatic.Description,
                ImageUrl = viatic.ImageUrl,
                InvoiceDate = viatic.InvoiceDate,
                InvoiceAmmount = viatic.InvoiceAmmount,
                ViaticName = viatic.ViaticName,
                ViaticType = viatic.ViaticType,
                Traveler = viatic.Traveler,
                TripId = viatic.Trip.Id
            };
        }
    }
}
