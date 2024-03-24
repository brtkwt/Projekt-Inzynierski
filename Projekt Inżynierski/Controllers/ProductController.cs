using Microsoft.AspNetCore.Mvc;
using Projekt_Inżynierski.Entities;
using Projekt_Inżynierski.Interfaces;
using Projekt_Inżynierski.Helpers;
using AutoMapper;
using Projekt_Inżynierski.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Projekt_Inżynierski.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository,
         ICompanyRepository companyRepository, IMapper mapper, IImageService imageService)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _companyRepository = companyRepository;
            _mapper = mapper;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] ProductQueryObject query)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            int productNumber = await _productRepository.CountProductsQuery(query);
            var products = await _productRepository.GetProductsAsync(query);
            
            var productsDtos = _mapper.Map<IReadOnlyList<ProductReturnDto>>(products);

            var response = new
            {
                ObjectList = productsDtos,
                PageNumber = query.PageNumber,
                TotalPages = (int)Math.Ceiling( (decimal)productNumber / query.PageSize),
                PageSize = query.PageSize
            };

            return Ok(response);
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productRepository.GetProductByIdAsync(id);

            if (product == null)
                return NotFound();

            var productDto = _mapper.Map<ProductReturnDto>(product);

            return Ok(productDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductRequestDto productRequestDto)
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

            if (category == null)
            {
                return BadRequest("Category with this id doesn't exist !");
            }

            if (company == null)
            {
                return BadRequest("Company with this id doesn't exist !");
            }
            
            var newFileName = _imageService.SaveImage(productRequestDto.ImageFile);

            var newProduct = _mapper.Map<Product>(productRequestDto);
            newProduct.ImagePath = newFileName;
            newProduct.Category = category;
            newProduct.Company = company;

            var createdProduct = await _productRepository.CreateProductAsync( newProduct ) ;

            return Ok( _mapper.Map<ProductReturnDto>(createdProduct) );
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] UpdateProductDto updateProduct)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var staryProduct = await _productRepository.GetProductByIdAsync(id);
            if(staryProduct == null)
            {
                return NotFound();
            }

            if(await _productRepository.ProductNameExistsAsync(updateProduct.Name, id))
            {
                ModelState.AddModelError("productRequestDto.Name", "Product with this name already exists !");
                return BadRequest(ModelState);
            }

            var category = await _categoryRepository.GetCategoryByIdAsync(updateProduct.CategoryId);
            var company = await _companyRepository.GetCompanyByIdAsync(updateProduct.CompanyId);

            if(category == null)
            {
                return BadRequest("Category with this id doesn't exist !");
            }

            if(company == null)
            {
                return BadRequest("Company with this id doesn't exist !");
            }

            string image = staryProduct.ImagePath;
            if(updateProduct.ImageFile != null)
            {
                _imageService.UpdateImage(updateProduct.ImageFile, staryProduct.ImagePath);
            }

            var productToUpdate = _mapper.Map<Product>(updateProduct);
            productToUpdate.Category = category;
            productToUpdate.Company = company;
            productToUpdate.ImagePath = image;

            var updatedProduct = await _productRepository.UpdateProductAsync(id, productToUpdate );

            return Ok( _mapper.Map<ProductReturnDto>(updatedProduct) );
        }

        [Authorize(Roles = "Admin")]
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

            _imageService.DeleteImage(product.ImagePath);   // napewno?

            return NoContent();
        }
    }
}
