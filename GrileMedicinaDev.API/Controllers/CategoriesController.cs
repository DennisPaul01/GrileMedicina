using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GrileMedicinaDev.Models;
using GrileMedicinaDev.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using GrileMedicinaDev.Entities;

namespace GrileMedicinaDev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryEntity>>> GetCategories()
        {
            var categories = await _categoryRepository.GetCategoryAsync();
            return Ok(categories);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryEntity>> CreateCategory([FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdCategory = await _categoryRepository.CreateCategoryAsync(categoryDto);
            return CreatedAtAction(nameof(GetCategories), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(string id, [FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await _categoryRepository.UpdateCategoryAsync(id, categoryDto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var deleted = await _categoryRepository.DeleteCategoryAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
