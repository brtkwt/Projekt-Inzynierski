using Projekt_Inżynierski.Entities;

namespace Projekt_Inżynierski.Interfaces
{
    public interface ICompanyRepository
    {
        Task<Company> GetCompanyByIdAsync(int id);
        Task<IReadOnlyList<Company>> GetCompaniesAsync();
        Task<bool> CompanyNameExistsAsync(string newCompanyName);
        Task<Company> CreateCompanyAsync(string newCompanyName);
        Task<Company> UpdateCompanyAsync(int id, string newCompanyName);
        Task<int> CompanyHasProductsAsync(int id);
        Task<Company> DeleteCompanyAsync(int id);
    }
}
