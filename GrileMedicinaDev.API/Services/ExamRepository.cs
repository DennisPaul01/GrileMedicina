using System;
using System.Threading.Tasks;
using GrileMedicinaDev.Models;
using GrileMedicinaDev.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using GrileMedicinaDev.Data;
using System.Linq;

namespace GrileMedicinaDev.Services
{
    public class ExamRepository : IExamRepository
    {
        private readonly IMongoCollection<Exam> _exams;
        private readonly IQuestionsRepository _questionsRepository;
        private readonly IChaptersRepository _chaptersRepository;
        public ExamRepository(MongoDbService mongoDbService, IQuestionsRepository questionsRepository, IChaptersRepository chaptersRepository)
        {
            _exams = mongoDbService.Database?.GetCollection<Exam>("exams");
            _chaptersRepository = chaptersRepository;
            _questionsRepository = questionsRepository;
        }

        public async Task<Exam> CreateExamAsync(ExamForCreationDto examDto)
        {
            var chapterIds = examDto.Chapters.Select(c => c._id).ToList();
            var chaptersExist = await _chaptersRepository.DoChaptersExistAsync(chapterIds);

            if (!chaptersExist)
            {
                throw new KeyNotFoundException("One or more chapters do not exist.");
            }

            var chapters = new List<Chapter>();
            foreach (var chapterDto in examDto.Chapters)
            {
                var chapter = await _chaptersRepository.GetChapterByIdAsync(chapterDto._id);
                chapters.Add(chapter);
            }

            var exam = new Exam
            {
                ExamType = examDto.ExamType,
                Chapters = chapters,
                RandomizeQuestions = examDto.RandomizeQuestions,
                RandomizeAnswers = examDto.RandomizeAnswers,
                Quantity = examDto.Quantity,
                Timed = examDto.Timed,
                Timer = examDto.Timer,
                ShowAnswers = examDto.ShowAnswers
            };

            var questions = await _questionsRepository.GetQuestionsByChapterIdsAsync(chapterIds);
            exam.Questions = questions.ToArray();

            await _exams.InsertOneAsync(exam);
            return exam;
        }
    }
}
