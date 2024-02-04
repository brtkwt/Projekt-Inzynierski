using Microsoft.AspNetCore.Mvc;
using Projekt_Inżynierski.Entities;
using Projekt_Inżynierski.Entities.Dtos;
using Projekt_Inżynierski.Interfaces;
using Projekt_Inżynierski.Helpers;


namespace Projekt_Inżynierski.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICompanyRepository _companyRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, ICompanyRepository companyRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _companyRepository = companyRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IReadOnlyList<Product>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetProducts([FromQuery] QueryObject query)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var products = await _productRepository.GetProductsAsync(query);

            var productsDtos = products.Select( s => s.ToResponseDto() );

            return Ok(productsDtos);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(400), ProducesResponseType(404)]
        public async Task<IActionResult> GetProduct(int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productRepository.GetProductByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product.ToResponseDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequestDto productRequestDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(await _productRepository.ProductNameExistsAsync(productRequestDto.Name))
            {
                ModelState.AddModelError("productRequestDto", "Product with this name already exists !");
                return BadRequest(ModelState);
            }

            var category = await _categoryRepository.GetCategoryByIdAsync(productRequestDto.CategoryId);
            var company = await _companyRepository.GetCompanyByIdAsync(productRequestDto.CompanyId);

            if(category == null)
            {
                return BadRequest("Category with this id doesn't exist !");
            }

            if(company == null)
            {
                return BadRequest("Company with this id doesn't exist !");
            }
          
            var newProduct = await _productRepository.CreateProductAsync( productRequestDto.ToProductFromRequestDto(category, company) );

            // return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id}, newProduct.ToResponseDto() );
            return Ok( newProduct.ToResponseDto() );
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] CreateProductRequestDto productRequestDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(await _productRepository.ProductNameExistsAsync(productRequestDto.Name, id))
            {
                ModelState.AddModelError("productRequestDto.Name", "Product with this name already exists !");
                return BadRequest(ModelState);
            }

            var category = await _categoryRepository.GetCategoryByIdAsync(productRequestDto.CategoryId);
            var company = await _companyRepository.GetCompanyByIdAsync(productRequestDto.CompanyId);

            if(category == null)
            {
                return BadRequest("Category with this id doesn't exist !");
            }

            if(company == null)
            {
                return BadRequest("Company with this id doesn't exist !");
            }

            var updatedProduct = await _productRepository.UpdateProductAsync(id, productRequestDto, category, company);

            if(updatedProduct == null)
            {
                return NotFound();
            }

            return Ok(updatedProduct.ToResponseDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productRepository.DeleteProductAsync(id);
            
            if(product == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
