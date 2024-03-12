using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projekt_Inżynierski.Entities.Order;

namespace Projekt_Inżynierski.Dtos
{
    public class OrderPatchDto
    {
        public string PatchClientEmail { get; set; }
        public int? PatchShippingMethodId { get; set; }
        public ShippingAddressDto PatchOrderShippingAddress { get; set; }
        
    }
}