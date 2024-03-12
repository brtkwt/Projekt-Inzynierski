using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Configuration.Annotations;

namespace Projekt_Inżynierski.Entities.Order
{
    public class Order
    {
        public int Id { get; set; }
        public string ClientEmail { get; set; }
        public OrderShippingAddress OrderShippingAddress { get; set; }
        public ShippingMethod ShippingMethod { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public decimal SumCost { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now; 
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Created;
        public string PayIntentId { get; set; }
        
        public decimal GetFullCost()
        {
            return SumCost + ShippingMethod.ShippingFee;
        }

        public Order()
        {
        }
        public Order(string ClientEmail, IReadOnlyList<OrderItem> OrderItems, decimal SumCost, OrderShippingAddress OrderShippingAddress, ShippingMethod ShippingMethod)
        {
            this.ClientEmail = ClientEmail;
            this.OrderItems = OrderItems;
            this.SumCost = SumCost;
            this.OrderShippingAddress = OrderShippingAddress;
            this.ShippingMethod = ShippingMethod;
        }
    }
}