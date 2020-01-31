using System;
using System.ComponentModel.DataAnnotations;

namespace GoViatic.Common.Models
{
    public class ViaticRequest
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal InvoiceAmmount { get; set; }
        public string ViaticType { get; set; }
        public int TripId { get; set; }
        public int TravelerId { get; set; }
        public byte[] ImageArray { get; set; }
    }
}
