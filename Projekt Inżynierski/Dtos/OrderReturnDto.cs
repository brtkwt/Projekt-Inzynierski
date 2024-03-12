using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projekt_Inżynierski.Entities.Order;

namespace Projekt_Inżynierski.Dtos
{
    public class OrderReturnDto
    {
        public int Id { get; set; }
        public string ClientEmail { get; set; }
        public OrderShippingAddress OrderShippingAddress { get; set; }
        public string ShippingMethod { get; set; }
        public decimal ShippingCost { get; set; }
        public IReadOnlyList<OrderItemDto> OrderItemDtos { get; set; }
        public decimal SumCost { get; set; }
        public decimal FullCost { get; set; }
        public DateTime OrderDate { get; set; } 
        public string OrderStatus { get; set; }
    }
}