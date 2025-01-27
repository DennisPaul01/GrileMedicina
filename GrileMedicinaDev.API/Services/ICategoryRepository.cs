using GrileMedicinaDev.Entities;
using GrileMedicinaDev.Models;

namespace GrileMedicinaDev.Services
{
    public interface ICategoryRepository
    {
        Task<List<CategoryEntity>> GetCategoryAsync();
        Task<CategoryEntity> CreateCategoryAsync(CategoryDto category);
        Task<bool> UpdateCategoryAsync(string id, CategoryDto category);
        Task<bool> DeleteCategoryAsync(string id);
        Task<(bool, string)> CheckCategoriesExistAsync(string[] categoryNames);
    }
}
