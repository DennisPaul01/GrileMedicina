using MongoDB.Driver;
using GrileMedicinaDev.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using GrileMedicinaDev.Data;
using GrileMedicinaDev.Models;
using MongoDB.Bson;

namespace GrileMedicinaDev.Services
{
    public class QuestionsRepository : IQuestionsRepository
    {
        private readonly IMongoCollection<Question> _questions;

        public QuestionsRepository(MongoDbService mongoDbService)
        {
            _questions = mongoDbService.Database?.GetCollection<Question>("question");
        }

        public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            return await _questions.Find(FilterDefinition<Question>.Empty).ToListAsync();
        }

        public async Task<Question?> GetQuestionByIdAsync(string id)
        {
            var filter = Builders<Question>.Filter.Eq(x => x.Id, id);
            return await _questions.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateQuestionAsync(Question question)
        {
            await _questions.InsertOneAsync(question);
        }

        public async Task<bool> UpdateQuestionAsync(string id, Question question)
        {
            var filter = Builders<Question>.Filter.Eq(x => x.Id, id);
            var update = Builders<Question>.Update
                // ...existing code...
                .Set(x => x.Answers, question.Answers);
            var result = await _questions.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteQuestionAsync(string id)
        {
            var filter = Builders<Question>.Filter.Eq(x => x.Id, id);
            var result = await _questions.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }

        public async Task<Question> CreateQuestionFromDtoAsync(QuestionsForCreationDto questionDto)
        {
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

            await CreateQuestionAsync(question);
            return question;
        }
    }
}
