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

        public ChapterController(IChaptersRepository chaptersRepository)
        {
            _chaptersRepository = chaptersRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Chapter>> GetAllChapters()
        {
            return await _chaptersRepository.GetAllChaptersAsync();
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
