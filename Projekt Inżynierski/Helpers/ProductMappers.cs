using Projekt_Inżynierski.Entities;
using Projekt_Inżynierski.Entities.Dto;
using Projekt_Inżynierski.Entities.Dtos;

namespace Projekt_Inżynierski.Helpers
{
    public static class ProductMappers
    {
        public static ProductToReturnDto ToResponseDto(this Product productModel)
        {
            return new ProductToReturnDto
            {
                Id = productModel.Id,
                Name = productModel.Name,
                Price = productModel.Price,
                Description = productModel.Description,
                ImagePath = productModel.ImagePath,

                CategoryName = productModel.Category.Name,
                CompanyName = productModel.Company.Name
            };
        }
        
        public static Product ToProductFromRequestDto(this CreateProductRequestDto productRequest,Category CategoryDb , Company CompanyDB)
        {
            return new Product
            {
                Name = productRequest.Name,
                Price = productRequest.Price,
                Description = productRequest.Description,
                ImagePath = productRequest.ImagePath,
                CategoryId = productRequest.CategoryId,
                CompanyId = productRequest.CompanyId,

                Company = CompanyDB,
                Category = CategoryDb
            };
        }
        
    }
}