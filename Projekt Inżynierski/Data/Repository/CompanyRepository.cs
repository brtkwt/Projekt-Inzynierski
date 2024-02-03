using Microsoft.EntityFrameworkCore;
using Projekt_Inżynierski.Entities;
using Projekt_Inżynierski.Interfaces;

namespace Projekt_Inżynierski.Data.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DataContext _context;

        public CompanyRepository(DataContext context)
        {
            _context = context;
        }


        public async Task<IReadOnlyList<Company>> GetCompaniesAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company> GetCompanyByIdAsync(int id)
        {
            return await _context.Companies.FindAsync(id);
        }

        public async Task<bool> CompanyNameExistsAsync(string newCompanyName)
        {
            return await _context.Companies.AnyAsync(c => c.Name == newCompanyName);
        }

        public async Task<Company> CreateCompanyAsync(string newCompanyName)
        {
            Company newCompany = new Company()
            {
                Name = newCompanyName
            };

            await _context.Companies.AddAsync(newCompany);
            await _context.SaveChangesAsync();

            return newCompany;
        }

        public async Task<Company> UpdateCompanyAsync(int id, string newCompanyName)
        {
            var existingCompany = await _context.Companies.FindAsync(id);

            if(existingCompany == null)
            {
                return null;    // ?????
            }

            existingCompany.Name = newCompanyName;
            await _context.SaveChangesAsync();

            return existingCompany;
        }

        public async Task<int> CompanyHasProductsAsync(int id)
        {
            var existingCompany = await _context.Companies.FindAsync(id);

            if(existingCompany == null)
            {
                return -1;    // ?????
            }

            return await _context.Products.CountAsync( x => x.CompanyId == id);
        }

        public async Task<Company> DeleteCompanyAsync(int id)
        {
            var existingCompany = await _context.Companies.FindAsync(id);

            _context.Companies.Remove(existingCompany);
            await _context.SaveChangesAsync();

            return existingCompany;
        }
    }
}
