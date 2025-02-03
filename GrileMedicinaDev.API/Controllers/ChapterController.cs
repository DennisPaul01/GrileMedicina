using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using GrileMedicinaDev.Entities;
using GrileMedicinaDev.Models;
using GrileMedicinaDev.Services;

namespace GrileMedicinaDev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChapterController : ControllerBase
    {
        private readonly IChaptersRepository _chaptersRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ChapterController(IChaptersRepository chaptersRepository, ICategoryRepository categoryRepository)
        {
            _chaptersRepository = chaptersRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllChapters(
            [FromQuery] string? name,
            [FromQuery] string? createdBy,
            [FromQuery] bool? isUserContent,
            [FromQuery] bool? explanationsGenerating,
            [FromQuery] List<string>? categories,
            [FromQuery] int? quantity,
            [FromQuery] List<string>? pages
            )
        {
            if (categories != null && categories.Any())
            {
                var (exists, missingCategory) = await _categoryRepository.CheckCategoriesExistAsync(categories.ToArray());
                if (!exists)
                {
                    return NotFound($"Category '{missingCategory}' does not exist.");
                }
            }

            var chapters = await _chaptersRepository.GetAllChaptersAsync(name, createdBy, isUserContent, explanationsGenerating, categories, quantity, pages);
            return Ok(chapters);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Chapter?>> GetChapterById(string id)
        {
            var chapter = await _chaptersRepository.GetChapterByIdAsync(id);
            return chapter is not null ? Ok(chapter) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Chapter>> CreateChapter([FromBody] ChapterForCreationDto chapterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var chapter = await _chaptersRepository.CreateChapterFromDtoAsync(chapterDto);
            return CreatedAtAction(nameof(GetChapterById), new { id = chapter.Id }, chapter);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChapter(string id, Chapter chapter)
        {
            var updated = await _chaptersRepository.UpdateChapterAsync(id, chapter);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChapter(string id)
        {
            var deleted = await _chaptersRepository.DeleteChapterAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
