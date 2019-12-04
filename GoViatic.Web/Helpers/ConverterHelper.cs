using GoViatic.Web.Data;
using GoViatic.Web.Data.Entities;
using GoViatic.Web.Models;
using System.Threading.Tasks;

namespace GoViatic.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _dataContext;

        public ConverterHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Trip> ToTripAsync (TripViewModel model, bool isNew)
        {
            return new Trip
            {
                Id = isNew ? 0 : model.Id,
                City = model.City,
                Date = model.Date.ToUniversalTime(),
                EndDate = model.EndDate.ToUniversalTime(),
                Traveler = await _dataContext.Travelers.FindAsync(model.TravelerId),
                Viatics = model.Viatics
            };
        }
        
        
        
        public async Task<Viatic> ToViaticAsync(ViaticViewModel model, string path)
        {
            return new Viatic
            {
                Id = model.Id,
                Description = model.Description,
                ImageUrl = path,
                InvoiceDate = model.InvoiceDate,
                ViaticName = model.ViaticName,
                Traveler = await _dataContext.Travelers.FindAsync(model.Traveler),
                ViaticType = await _dataContext.ViaticTypes.FindAsync(model.ViaticTypeId),
                Trip = await _dataContext.Trips.FindAsync(model.TripId),
            };
        }
    }
}
