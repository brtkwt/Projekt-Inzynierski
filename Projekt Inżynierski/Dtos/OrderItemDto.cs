using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Inżynierski.Dtos
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public int ProductNumber { get; set; }
        public string ImageUrl { get; set; }
    }
}