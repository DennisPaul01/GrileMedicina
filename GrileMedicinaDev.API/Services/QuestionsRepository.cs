using MongoDB.Driver;
using GrileMedicinaDev.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using GrileMedicinaDev.Data;
using GrileMedicinaDev.Models;
using MongoDB.Bson;
using GrileMedicinaDev.Exceptions;

namespace GrileMedicinaDev.Services
{
    public class QuestionsRepository : IQuestionsRepository
    {
        private readonly IMongoCollection<Question> _questions;
        private readonly IChaptersRepository _chaptersRepository;

        public QuestionsRepository(MongoDbService mongoDbService, IChaptersRepository chaptersRepository)
        {
            _questions = mongoDbService.Database?.GetCollection<Question>("question");
            _chaptersRepository = chaptersRepository;
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
            var chapterExists = await _chaptersRepository.DoChaptersExistAsync(new List<string> { question.ChapterId });
            if (!chapterExists)
            {
                throw new NotFoundException("ChapterId does not exist.");
            }

            var count = CountQuestionsByChapterIdAsync(question.ChapterId).Result;
            await _chaptersRepository.UpdateQuantityChapterAsync(question.ChapterId, count + 1);
            await _questions.InsertOneAsync(question);
        }

        public async Task<bool> UpdateQuestionAsync(string id, QuestionForUpdate question)
        {
            var filter = Builders<Question>.Filter.Eq(x => x.Id, id);
            var existingQuestion = await _questions.Find(filter).FirstOrDefaultAsync();

            if (existingQuestion == null)
            {
                return false;
            }

            if (question.ChapterId != null)
            {
                var chapterExists = await _chaptersRepository.DoChaptersExistAsync(new List<string> { question.ChapterId });
                if (!chapterExists)
                {
                    throw new KeyNotFoundException("ChapterId does not exist.");
                }
            }

            var update = Builders<Question>.Update
                .Set(x => x.ChapterId, question.ChapterId ?? existingQuestion.ChapterId)
                .Set(x => x.Pages, question.Pages ?? existingQuestion.Pages)
                .Set(x => x.ExplanationExists, question.ExplanationExists ?? existingQuestion.ExplanationExists)
                .Set(x => x.Text, question.Text ?? existingQuestion.Text);

            if (question.Answers != null && question.Answers.Any())
            {
                var answers = question.Answers.Select(a => new Answer
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    AnswerText = a.AnswerText,
                    IsCorrect = a.IsCorrect,
                }).ToList();

                var correctAnswerIds = answers.Where(a => a.IsCorrect).Select(a => a.Id).ToArray();

                update = update
                    .Set(x => x.Answers, answers)
                    .Set(x => x.CorrectAnswers, correctAnswerIds.ToList())
                    .Set(x => x.AnswersCount, question.Answers.Count)
                    .Set(x => x.CorrectAnswersCount, question.Answers.Count(a => a.IsCorrect));
            }

            var result = await _questions.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteQuestionAsync(string id)
        {
            var filter = Builders<Question>.Filter.Eq(x => x.Id, id);
            var question = await _questions.Find(filter).FirstOrDefaultAsync();

            if (question == null)
            {
                return false;
            }

            var count = await CountQuestionsByChapterIdAsync(question.ChapterId);
            await _chaptersRepository.UpdateQuantityChapterAsync(question.ChapterId, count - 1);

            var result = await _questions.DeleteOneAsync(filter);

            return result.DeletedCount > 0;
        }

        public async Task<Question> CreateQuestionFromDtoAsync(QuestionsForCreationDto questionDto)
        {
            var chapterExists = await _chaptersRepository.DoChaptersExistAsync(new List<string> { questionDto.ChapterId });
            if (!chapterExists)
            {
                throw new NotFoundException("ChapterId does not exist.");
            }

            var answers = questionDto.Answers.Select(a => new Answer
            {
                Id = ObjectId.GenerateNewId().ToString(),
                AnswerText = a.AnswerText,
                IsCorrect = a.IsCorrect,
            }).ToList();

            var correctAnswerIds = answers.Where(a => a.IsCorrect).Select(a => a.Id).ToArray();

            var question = new Question
            {
                Id = ObjectId.GenerateNewId().ToString(),
                ChapterId = questionDto.ChapterId,
                Pages = questionDto.Pages,
                CorrectAnswers = correctAnswerIds.ToList(),
                AnswersCount = questionDto.Answers.Count,
                CorrectAnswersCount = questionDto.Answers.Count(a => a.IsCorrect),
                ExplanationExists = questionDto.ExplanationExists,
                Text = questionDto.Text,
                Answers = answers
            };

            await CreateQuestionAsync(question);
            return question;
        }

        public async Task<IEnumerable<Question>> GetQuestionsByChapterIdsAsync(IEnumerable<string> chapterIds)
        {
            var filter = Builders<Question>.Filter.In(q => q.ChapterId, chapterIds);
            return await _questions.Find(filter).ToListAsync();
        }

        private async Task<int> CountQuestionsByChapterIdAsync(string chapterId)
        {
            var filter = Builders<Question>.Filter.Eq(q => q.ChapterId, chapterId);
            return (int)await _questions.CountDocumentsAsync(filter);
        }
    }
}
