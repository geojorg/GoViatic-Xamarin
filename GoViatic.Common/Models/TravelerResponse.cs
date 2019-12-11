using System;
using System.Collections.Generic;

namespace GoViatic.Common.Models
{
    public class TravelerResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Company { get; set; }
        public string Document { get; set; }
        public ICollection<TripResponse> Trips { get; set; }
    }
}
