using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Inżynierski.Entities.Order
{
    public class OrderItem
    {
        public int Id { get; set; }
        public ProductOrdered ProductOrdered { get; set; }
        public decimal Cost { get; set; }
        public int ProductNumber { get; set; }

        public OrderItem()
        {
        }
        public OrderItem(ProductOrdered ProductOrdered, decimal Cost, int ProductNumber)
        {
            this.ProductOrdered = ProductOrdered;
            this.Cost = Cost;
            this.ProductNumber = ProductNumber;
        }
    }
}