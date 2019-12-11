using System.ComponentModel.DataAnnotations;

namespace GoViatic.Common.Models
{
    public class EmailRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
