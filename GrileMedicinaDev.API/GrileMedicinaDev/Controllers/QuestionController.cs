using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using GrileMedicinaDev.Data;
using GrileMedicinaDev.Entities;
using GrileMedicinaDev.Models
using MongoDB.Bson;

namespace GrileMedicinaDev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        public readonly IMongoCollection<Question>? _questions;
        public QuestionController(MongoDbService mongoDbService)
        {
            _questions = mongoDbService.Database?.GetCollection<Question>("question");
        }


        [HttpGet]
        public async Task<IEnumerable<Question>> GetAlltQuestions()
        {
            var questions = await _questions.Find(FilterDefinition<Question>.Empty).ToListAsync();
            return questions;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Question?>> GetQuestionById(string id)
        {
            var filter = Builders<Question>.Filter.Eq(x=>x.Id, id);
            var question = _questions.Find(filter).FirstOrDefaultAsync();
            return question is not null ? Ok(question) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Question>> CreateQuestion([FromBody] QuestionsForCreationDto questionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Trimite erorile de validare către client
            }

            var question = new Question
            {
                Id = ObjectId.GenerateNewId().ToString(),
                ChapterId = questionDto.ChapterId,
                Pages = questionDto.Pages,
                CorrectAnswers = questionDto.CorrectAnswers,
                AnswersCount = questionDto.AnswersCount,
                CorrectAnswersCount = questionDto.CorrectAnswersCount,
                ExplanationExists = questionDto.ExplanationExists,
                QuestionIndex = questionDto.QuestionIndex,
                AnswersOrder = questionDto.AnswersOrder,
                Text = questionDto.Text,
                Answers = questionDto.Answers
            };

            await _questions.InsertOneAsync(question);
            return CreatedAtAction(nameof(GetQuestionById), new { id = question.Id }, question);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(string id, Question question)
        {
            var filter = Builders<Question>.Filter.Eq(x => x.Id, id);
            var update = Builders<Question>.Update
                .Set(x => x.ChapterId, question.ChapterId)
                .Set(x => x.Pages, question.Pages)
                .Set(x => x.CorrectAnswers, question.CorrectAnswers)
                .Set(x => x.AnswersCount, question.AnswersCount)
                .Set(x => x.CorrectAnswersCount, question.CorrectAnswersCount)
                .Set(x => x.ExplanationExists, question.ExplanationExists)
                .Set(x => x.QuestionIndex, question.QuestionIndex)
                .Set(x => x.AnswersOrder, question.AnswersOrder)
                .Set(x => x.Text, question.Text)
                .Set(x => x.Answers, question.Answers);
            var result = await _questions.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0 ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(string id)
        {
            var filter = Builders<Question>.Filter.Eq(x => x.Id, id);
            var result = await _questions.DeleteOneAsync(filter);
            return result.DeletedCount > 0 ? NoContent() : NotFound();
        }
    }
}
