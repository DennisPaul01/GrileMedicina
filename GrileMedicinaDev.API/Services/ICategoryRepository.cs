using GrileMedicinaDev.Entities;
using GrileMedicinaDev.Models;

namespace GrileMedicinaDev.Services
{
    public interface ICategoryRepository
    {
        Task<List<CategoryEntity>> GetCategoryAsync(string[] select, int limit, int? questionsCountPlatform, int? questionsCountPerCategory, int? cardsCountPlatform, int? cardsCountPerCategory, int? usersJoined, int? examsStarted, int? ordersCompleted, DateTime? examDate);
        Task<CategoryEntity> CreateCategoryAsync(CategoryDto category);
        Task<bool> UpdateCategoryAsync(string id, CategoryDto category);
        Task<bool> DeleteCategoryAsync(string id);
        Task<(bool, string)> CheckCategoriesExistAsync(string[] categoryNames);
    }
}
