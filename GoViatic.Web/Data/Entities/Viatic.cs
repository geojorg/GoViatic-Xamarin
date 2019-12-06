using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoViatic.Web.Data.Entities
{
    public class Viatic
    {
        public int Id { get; set; }

        [Display(Name = "Viatic")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string ViaticName { get; set; }

        [Display(Name = "Description")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Description { get; set; }

        [Display(Name = "Invoice Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime InvoiceDate { get; set; }

        [Display(Name = "Invoice Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime InvoiceDateLocal => InvoiceDate.ToLocalTime();

        [Display(Name = "Invoice Image")]
        public string ImageUrl { get; set; }

        [Display(Name = "Invoice Ammount")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public decimal InvoiceAmmount { get; set; }

        [Display(Name = "Viatic Type")]
        [Required]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string ViaticType { get; set; }

        //TODO Put the right Url
        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
            ? null
            : $"https://TBD";

        public Traveler Traveler { get; set; }
        public Trip Trip { get; set; }
    }
}
