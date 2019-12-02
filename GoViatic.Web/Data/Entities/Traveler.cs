using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoViatic.Web.Data.Entities
{
    public class Traveler
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [MaxLength(50)]
        public string Company { get; set; }

        public ICollection<Viatic> Viatics { get; set; }
        public ICollection<Trip> Trips { get; set; }
    }
}
