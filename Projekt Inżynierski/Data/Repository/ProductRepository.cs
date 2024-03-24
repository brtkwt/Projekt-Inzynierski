using Microsoft.EntityFrameworkCore;
using Projekt_Inżynierski.Entities;
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

        public async Task<IReadOnlyList<Product>> GetProductsAsync(ProductQueryObject query)
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
                case "costasc":
                    products = products.OrderBy(p => p.Cost);
                    break;
                case "costdesc":
                    products = products.OrderByDescending(p => p.Cost);
                    break;
                default:
                    products = products.OrderBy(p => p.Name);
                    break;
            }

            var skipNumber = query.PageSize * (query.PageNumber - 1);

            return await products.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<int> CountProductsQuery(ProductQueryObject query)
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

            return await products.CountAsync();
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

        public async Task<Product> UpdateProductAsync(int id, Product updatedProduct)
        {
            var existingProduct = await _context.Products.Include(p => p.Category).Include(p => p.Company).FirstOrDefaultAsync(x => x.Id == id);

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Cost = updatedProduct.Cost;
            existingProduct.Description = updatedProduct.Description;
            existingProduct.ImagePath = updatedProduct.ImagePath;
            existingProduct.CategoryId = updatedProduct.CategoryId;
            existingProduct.CompanyId = updatedProduct.CompanyId;
            existingProduct.Category = updatedProduct.Category;
            existingProduct.Company = updatedProduct.Company;

            _context.Update(existingProduct);

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
