using GrileMedicinaDev.Entities;
using GrileMedicinaDev.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GrileMedicinaDev.Services
{
    public interface ICategoryStatisticsService
    {
        Task<List<CategoryStatistics>> GetCategoryStatisticsAsync();
        Task<CategoryStatistics> CreateCategoryStatisticsAsync(CategoryStatisticsDto categoryStatistics);
        Task<bool> UpdateCategoryStatisticsAsync(string id, CategoryStatisticsDto categoryStatistics);
        Task<bool> DeleteCategoryStatisticsAsync(string id);
    }
}
