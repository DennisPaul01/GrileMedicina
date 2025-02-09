using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GrileMedicinaDev.Models;
using GrileMedicinaDev.Services;

namespace GrileMedicinaDev.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamController : ControllerBase
    {
        private readonly IExamRepository _examRepository;

        public ExamController(IExamRepository examRepository)
        {
            _examRepository = examRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateExam([FromBody] ExamForCreationDto examDto)
        {
            try
            {
                var exam = await _examRepository.CreateExamAsync(examDto);
                return CreatedAtAction(nameof(CreateExam), new { id = exam.Id }, exam);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
