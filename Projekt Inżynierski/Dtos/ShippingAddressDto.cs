using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Inżynierski.Dtos
{
    public class ShippingAddressDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        public string Country { get; set; }
        [Required]
        public string Voivodeship { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string BuildingNumber { get; set; }
        [Required]
        public string ZipCode { get; set; }
    }
}