using GoViatic.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace GoViatic.Web.Models
{
    public class ViaticViewModel : Viatic
    {
        public int TripId { get; set; }
        
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
