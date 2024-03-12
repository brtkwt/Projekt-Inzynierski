using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt_Inżynierski.Entities
{
    [Table("ShippingAddresses")]
    public class ShippingAddress
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Country { get; set; }
        public string Voivodeship { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string ZipCode { get; set; }

        [Required]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}