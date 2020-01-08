using System;
using System.Collections.Generic;

namespace GoViatic.Common.Models
{
    public class TripResponse
    {
        public int Id { get; set; }
        public string City { get; set; }
        public decimal Budget { get; set; }
        public DateTime Date { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<ViaticResponse> Viatics { get; set; }
    }
}
