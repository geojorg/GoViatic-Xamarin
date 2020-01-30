using System.Collections.Generic;

namespace GoViatic.Common.Models
{
    public class TravelerResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public ICollection<TripResponse> Trips { get; set; }
    }
}
