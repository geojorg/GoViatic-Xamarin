using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GoViatic.Web.Data.Entities
{
    public class User : IdentityUser
    {
        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string LastName { get; set; }

        [MaxLength(60, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Company { get; set; }
    }
}
