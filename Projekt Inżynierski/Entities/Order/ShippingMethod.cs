using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Inżynierski.Entities.Order
{
    public class ShippingMethod
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal ShippingFee { get; set; }
        public string EstimatedShippingTime { get; set; }
        public string Description { get; set; }
    }
}