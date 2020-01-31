using GoViatic.Common.Helpers;
using GoViatic.Common.Models;
using GoViatic.Web.Data;
using GoViatic.Web.Data.Entities;
using GoViatic.Web.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GoViatic.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ViaticsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public ViaticsController(DataContext context, IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }

        public async Task<IActionResult> PostViatic([FromBody] ViaticRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var traveler = await _context.Travelers.FindAsync(request.TravelerId);
            if (traveler == null)
            {
                return BadRequest("Not valid Traveler.");
            }

            var trip = await _context.Trips.FindAsync(request.TripId);
            if (trip == null)
            {
                return BadRequest("Not valid Trip.");
            }

            var imageUrl = string.Empty;
            if (request.ImageArray != null && request.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(request.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "wwwroot\\images\\Pets";
                var fullPath = $"~/images/Pets/{file}";
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    imageUrl = fullPath;
                }
            }

            var viatic = new Viatic
            {
                ViaticName = request.Name,
                Description = request.Description,
                InvoiceDate = request.InvoiceDate.ToUniversalTime(),
                ImageUrl = imageUrl,
                InvoiceAmmount = request.InvoiceAmmount,
                ViaticType = request.ViaticType,
                Traveler = traveler,
                Trip = trip
            };

            _context.Viatics.Add(viatic);
            await _context.SaveChangesAsync();
            return Ok(_converterHelper.ToViaticResponse(viatic));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutViatic([FromRoute] int id, [FromBody] ViaticRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.Id)
            {
                return BadRequest();
            }

            var oldViatic = await _context.Viatics.FindAsync(request.Id);
            if (oldViatic == null)
            {
                return BadRequest("Viatic doesn't exists.");
            }

            var imageUrl = oldViatic.ImageUrl;
            if (request.ImageArray != null && request.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(request.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "wwwroot\\images\\Pets";
                var fullPath = $"~/images/Pets/{file}";
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    imageUrl = fullPath;
                }
            }

            oldViatic.ViaticName = request.Name;
            oldViatic.Description = request.Description;
            oldViatic.InvoiceDate = request.InvoiceDate.ToUniversalTime();
            oldViatic.ImageUrl = imageUrl;
            oldViatic.InvoiceAmmount = request.InvoiceAmmount;
            _context.Viatics.Update(oldViatic);
            await _context.SaveChangesAsync();
            return Ok(_converterHelper.ToViaticResponse(oldViatic));
        }

        //DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteViatic([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var viatic = await _context.Viatics
                .Include(p => p.Trip)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (viatic == null)
            {
                return this.NotFound();
            }

            _context.Viatics.Remove(viatic);
            await _context.SaveChangesAsync();
            return Ok("Viatic deleted");
        }
    }
}