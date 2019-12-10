using GoViatic.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
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
