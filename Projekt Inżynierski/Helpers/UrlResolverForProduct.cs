using AutoMapper;
using Projekt_Inżynierski.Dtos;
using Projekt_Inżynierski.Entities;

namespace Projekt_Inżynierski.Helpers
{
    public class UrlResolverForProduct : IValueResolver<Product, ProductReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public UrlResolverForProduct(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product product, ProductReturnDto productReturnDto, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(product.ImagePath) )
            {
                string fullURL = _configuration["ApiWebAdress"] + product.ImagePath;
                return fullURL;
            }
            return null;
        }
    }
}