using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Inżynierski.Entities.Order
{
    public class ProductOrdered
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImagePath { get; set; }

        public ProductOrdered(int ProductId, string ProductName, string ImagePath)
        {
            this.ProductId = ProductId;
            this.ProductName = ProductName;
            this.ImagePath = ImagePath;
        }
    }
}