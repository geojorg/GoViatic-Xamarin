using GoViatic.Common.Models;
using GoViatic.Web.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GoViatic.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TravelersController : ControllerBase
    {
        private readonly DataContext _context;

        public TravelersController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("GetTravelerByEmail")]
        public async Task<IActionResult> GetTraveler(EmailRequest emailRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var traveler = await _context.Travelers
                .Include(t =>t.User)
                .Include(t =>t.Trips)
                .ThenInclude(t =>t.Viatics)
                .FirstOrDefaultAsync(t => t.User.UserName.ToLower() == emailRequest.Email.ToLower());

            var response = new TravelerResponse
            {
                Id = traveler.Id,
                Email = traveler.User.Email,
                FirstName = traveler.User.FirstName,
                Company = traveler.User.Company,
                Document = traveler.User.Document,
                Trips = traveler.Trips.Select(tr => new TripResponse
                {
                    Id = tr.Id,
                    City = tr.City,
                    Date = tr.Date,
                    EndDate = tr.EndDate,
                    Budget = tr.Bugdet,
                    Viatics = tr.Viatics.Select(v => new ViaticResponse
                    {
                        Id = v.Id,
                        ViaticType = v.ViaticType,
                        Description = v.Description,
                        InvoiceAmmount = v.InvoiceAmmount,
                        InvoiceDate = v.InvoiceDate,
                        ImageUrl = v.ImageFullPath,
                        Name = v.ViaticName,
                    }).ToList()
                }).ToList()
            };
            return Ok(response);
        }
    }
}
