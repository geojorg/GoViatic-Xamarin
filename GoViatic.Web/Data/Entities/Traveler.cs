using System.Collections.Generic;

namespace GoViatic.Web.Data.Entities
{
    public class Traveler
    {
        public int Id { get; set; }
        public User User { get; set; }
        public ICollection<Viatic> Viatics { get; set; }
        public ICollection<Trip> Trips { get; set; }
    }
}
