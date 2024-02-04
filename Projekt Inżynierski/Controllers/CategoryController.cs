using Microsoft.AspNetCore.Mvc;
using Projekt_Inżynierski.Entities;
using Projekt_Inżynierski.Helpers;
using Projekt_Inżynierski.Interfaces;

namespace Projekt_Inżynierski.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IReadOnlyList<Category>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCategories()
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var categories = await _categoryRepository.GetCategoriesAsync();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400), ProducesResponseType(404)]
        public async Task<IActionResult> GetCategory(int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var category = await _categoryRepository.GetCategoryByIdAsync(id);

            if (category == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] string newCategoryName)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(await _categoryRepository.CategoryNameExistsAsync(newCategoryName))
            {
                ModelState.AddModelError("newCategoryName", "Category with this name already exists !");
                return BadRequest(ModelState);
            }
            var newCategory = await _categoryRepository.CreateCategoryAsync(newCategoryName);

            return Ok(newCategory);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] string newCategoryName)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(await _categoryRepository.CategoryNameExistsAsync(newCategoryName))
            {
                ModelState.AddModelError("newCategoryName", "Category with this name already exists !");
                return BadRequest(ModelState);
            }

            var updatedCategory = await _categoryRepository.UpdateCategoryAsync(id, newCategoryName);

            if(updatedCategory == null)
            {
                return NotFound();
            }

            return Ok(updatedCategory);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            int activeProducts = await _categoryRepository.CategoryHasProductsAsync(id);
            
            if(activeProducts == -1)
            {
                return NotFound();
            }
            else if(activeProducts != 0 && activeProducts > 0 )
            {
                ModelState.AddModelError("", $"Cannot delete category with {activeProducts} active product/s !");
                return BadRequest(ModelState);
            }
            
            var category = await _categoryRepository.DeleteCategoryAsync(id);

            return NoContent();
        }
    }
}
