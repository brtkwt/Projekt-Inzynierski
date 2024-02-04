using Microsoft.EntityFrameworkCore;
using Projekt_Inżynierski.Entities;
using Projekt_Inżynierski.Entities.Dtos;
using Projekt_Inżynierski.Helpers;
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

        public async Task<IReadOnlyList<Product>> GetProductsAsync(QueryObject query)
        {
            var products = _context.Products.Include(p => p.Category).Include(p => p.Company).AsQueryable();

            if(!string.IsNullOrWhiteSpace(query.NameSearch))
            {
                products = products.Where(c => c.Name.Contains(query.NameSearch));
            }

            if (query.CategoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == query.CategoryId);
            }

            if (query.CompanyId.HasValue)
            {
                products = products.Where(p => p.CompanyId == query.CompanyId);
            }

            switch (query.SortBy)
            {
                case "priceasc":
                    products = products.OrderBy(p => p.Price);
                    break;
                case "pricedesc":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                default:
                    products = products.OrderBy(p => p.Name);
                    break;
            }

            var skipNumber = query.PageSize * (query.PageNumber - 1);

            return await products.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<bool> ProductNameExistsAsync(string newProductName, int id = 0)
        {
            if (id != 0)
                return await _context.Products.AnyAsync(c => c.Name == newProductName && c.Id != id);
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
