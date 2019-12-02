using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoViatic.Web.Data.Entities
{
    public class ViaticType
    {
        public int Id { get; set; }

        [Required]
        public string Concept { get; set; }

        public ICollection<Viatic> Viatics { get; set; }
    }
}
