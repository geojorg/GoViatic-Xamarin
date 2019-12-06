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
        public async Task<Trip> ToTripAsync (TripViewModel model, bool isNew)
        {
            return new Trip
            {
                Id = isNew ? 0 : model.Id,
                City = model.City,
                Date = model.Date.ToUniversalTime(),
                EndDate = model.EndDate.ToUniversalTime(),
                Traveler = await _context.Travelers.FindAsync(model.TravelerId),
                Viatics = model.Viatics
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
                ViaticName = model.ViaticName,
                ViaticType = model.ViaticType,
                Traveler = await _context.Travelers.FindAsync(model.Traveler),
                Trip = await _context.Trips.FindAsync(model.TripId),

            };
            return viatic;
        }
    }
}
