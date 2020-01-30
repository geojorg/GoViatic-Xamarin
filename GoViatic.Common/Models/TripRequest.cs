using System;
using System.ComponentModel.DataAnnotations;

namespace GoViatic.Common.Models
{
    public class TripRequest
    {
        public int Id { get; set; }
        [Required]
        public string City { get; set; }
        public decimal Budget { get; set; }
        public DateTime Date { get; set; }
        public DateTime EndDate { get; set; }
        public int TravelerId { get; set; }
    }
}
