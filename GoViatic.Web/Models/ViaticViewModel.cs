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

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Viatic Type")]
        public int ViaticTypeId { get; set; }

        public IEnumerable<SelectListItem> ViaticTypes { get; set; }

        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
