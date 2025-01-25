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
    public class CategoryStatisticsController : ControllerBase
    {
        private readonly ICategoryStatisticsService _categoryStatisticsService;

        public CategoryStatisticsController(ICategoryStatisticsService categoryStatisticsService)
        {
            _categoryStatisticsService = categoryStatisticsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryStatistics>>> GetCategoryStatistics()
        {
            var statistics = await _categoryStatisticsService.GetCategoryStatisticsAsync();
            return Ok(statistics);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryStatistics>> CreateCategoryStatistics([FromBody] CategoryStatisticsDto categoryStatisticsDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdStatistics = await _categoryStatisticsService.CreateCategoryStatisticsAsync(categoryStatisticsDto);
            return CreatedAtAction(nameof(GetCategoryStatistics), new { id = createdStatistics.Id }, createdStatistics);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryStatistics(string id, [FromBody] CategoryStatisticsDto categoryStatisticsDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await _categoryStatisticsService.UpdateCategoryStatisticsAsync(id, categoryStatisticsDto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryStatistics(string id)
        {
            var deleted = await _categoryStatisticsService.DeleteCategoryStatisticsAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
