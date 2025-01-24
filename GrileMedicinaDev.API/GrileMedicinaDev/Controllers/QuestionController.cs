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
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionsRepository _questionsRepository;

        public QuestionController(IQuestionsRepository questionsRepository)
        {
            _questionsRepository = questionsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Question>> GetAllQuestions()
        {
            return await _questionsRepository.GetAllQuestionsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Question?>> GetQuestionById(string id)
        {
            var question = await _questionsRepository.GetQuestionByIdAsync(id);
            return question is not null ? Ok(question) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Question>> CreateQuestion([FromBody] QuestionsForCreationDto questionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var question = await _questionsRepository.CreateQuestionFromDtoAsync(questionDto);
            return CreatedAtAction(nameof(GetQuestionById), new { id = question.Id }, question);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(string id, Question question)
        {
            var updated = await _questionsRepository.UpdateQuestionAsync(id, question);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(string id)
        {
            var deleted = await _questionsRepository.DeleteQuestionAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}


using System;
public class Kata
{
  public static string FindNeedle(object[] haystack)
  {
    var foundString = haystack.FirstOrDefault(el=>el.Equals("needle"));
    Console.Write(foundString);
    return "found the needle";
  }
}