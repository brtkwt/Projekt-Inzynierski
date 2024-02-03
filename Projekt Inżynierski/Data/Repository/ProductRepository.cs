using Microsoft.EntityFrameworkCore;
using Projekt_Inżynierski.Entities;
using Projekt_Inżynierski.Entities.Dtos;
using Projekt_Inżynierski.Interfaces;

namespace Projekt_Inżynierski.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }


        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.Include(p => p.Category).Include(p => p.Company).FirstOrDefaultAsync( x => x.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Products.Include(p => p.Category).Include(p => p.Company).ToListAsync();
        }

        public async Task<bool> ProductNameExistsAsync(string newProductName)
        {
            return await _context.Products.AnyAsync(c => c.Name == newProductName);
        }

        public async Task<Product> CreateProductAsync(Product newProduct)
        {
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();

            return newProduct;
        }

        public async Task<Product> UpdateProductAsync(int id, CreateProductRequestDto productRequestDto, Category categoryDB, Company companyDB)
        {
            var existingProduct = await _context.Products.FindAsync(id);

            if(existingProduct == null)
            {
                return null;
            }

            existingProduct.Name = productRequestDto.Name;
            existingProduct.Price = productRequestDto.Price;
            existingProduct.Description = productRequestDto.Description;
            existingProduct.ImagePath = productRequestDto.ImagePath;
            existingProduct.CategoryId = productRequestDto.CategoryId;
            existingProduct.CompanyId = productRequestDto.CompanyId;
            existingProduct.Category = categoryDB;
            existingProduct.Company = companyDB;

            await _context.SaveChangesAsync();

            return existingProduct;
        }

        public async Task<Product> DeleteProductAsync(int id)
        {
            var existingProduct = await _context.Products.FindAsync(id);

            if(existingProduct == null)
                return null;

            _context.Products.Remove(existingProduct);
            await _context.SaveChangesAsync();

            return existingProduct;
        }
    }
}
