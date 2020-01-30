using System.ComponentModel.DataAnnotations;

namespace GoViatic.Common.Models
{
    public class UserRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(20, MinimumLength =6)]
        public string Password { get; set; }
    }
}
