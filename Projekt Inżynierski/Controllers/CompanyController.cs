using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projekt_Inżynierski.Entities;
using Projekt_Inżynierski.Interfaces;

namespace Projekt_Inżynierski.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var companies = await _companyRepository.GetCompaniesAsync();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(companies);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCompany(int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var company = await _companyRepository.GetCompanyByIdAsync(id);

            if (company == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(company);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] string newCompanyName)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(await _companyRepository.CompanyNameExistsAsync(newCompanyName))
            {
                ModelState.AddModelError("newCompanyName", "Company with this name already exists !");
                return BadRequest(ModelState);
            }
            var newCompany = await _companyRepository.CreateCompanyAsync(newCompanyName);

            return Ok(newCompany);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] string newCompanyName)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(await _companyRepository.CompanyNameExistsAsync(newCompanyName))
            {
                ModelState.AddModelError("newCompanyName", "Company with this name already exists !");
                return BadRequest(ModelState);
            }

            var updatedCompany = await _companyRepository.UpdateCompanyAsync(id, newCompanyName);

            if(updatedCompany == null)
            {
                return NotFound();
            }

            return Ok(updatedCompany);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
                
            int activeProducts = await _companyRepository.CompanyHasProductsAsync(id);
            
            if(activeProducts == -1)
            {
                return NotFound();
            }
            else if(activeProducts != 0 && activeProducts > 0 )
            {
                ModelState.AddModelError("", $"Cannot delete company with {activeProducts} active product/s !");
                return BadRequest(ModelState);
            }
            
            var company = await _companyRepository.DeleteCompanyAsync(id);

            return NoContent();
        }
    }
}
