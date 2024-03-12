using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Projekt_Inżynierski.Dtos;
using Projekt_Inżynierski.Entities.Order;

namespace Projekt_Inżynierski.Helpers
{
    public class UrlResolverForOrderItem : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;
        public UrlResolverForOrderItem(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(OrderItem orderItem, OrderItemDto orderItemDto, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(orderItem.ProductOrdered.ImagePath) )
            {
                string fullURL = _configuration["ApiWebAdress"] + orderItem.ProductOrdered.ImagePath;
                return fullURL;
            }
            return null;
        }
    }
}