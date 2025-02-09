using GrileMedicinaDev.Entities;
using GrileMedicinaDev.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrileMedicinaDev.Services
{
    public interface IQuestionsRepository
    {
        Task<IEnumerable<Question>> GetAllQuestionsAsync();
        Task<Question?> GetQuestionByIdAsync(string id);
        Task CreateQuestionAsync(Question question);
        Task<bool> UpdateQuestionAsync(string id, QuestionForUpdate question);
        Task<bool> DeleteQuestionAsync(string id);
        Task<Question> CreateQuestionFromDtoAsync(QuestionsForCreationDto questionDto);
        Task<IEnumerable<Question>> GetQuestionsByChapterIdsAsync(IEnumerable<string> chapterIds);
    }
}
