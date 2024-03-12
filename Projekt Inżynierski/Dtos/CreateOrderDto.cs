using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Inżynierski.Dtos
{
    public class CreateOrderDto
    {
        [Required]
        public string cartId { get; set; }

        [Required]
        public int shippingMethodId { get; set; }

        [Required]
        public ShippingAddressDto ShippingAddressDto { get; set; }
    }
}