using System;

namespace GoViatic.Common.Models
{
    public class ViaticResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal InvoiceAmmount { get; set; }
        public string ViaticType { get; set; }
        public string ImageUrl { get; set; }
    }
}
