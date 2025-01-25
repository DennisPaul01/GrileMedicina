using GrileMedicinaDev.Entities;
using GrileMedicinaDev.Models;
using MongoDB.Driver;
using System.Threading.Tasks;
using GrileMedicinaDev.Data;

namespace GrileMedicinaDev.Services
{
    public class CategoryStatisticsService : ICategoryStatisticsService
    {
        private readonly IMongoCollection<CategoryStatistics> _categories;

        public CategoryStatisticsService(MongoDbService mongoDbService)
        {
            _categories = mongoDbService.Database?.GetCollection<CategoryStatistics>("categories");
        }

        public async Task<List<CategoryStatistics>> GetCategoryStatisticsAsync()
        {
              return await _categories.Find(FilterDefinition<CategoryStatistics>.Empty).ToListAsync();
        }

        public async Task<CategoryStatistics> CreateCategoryStatisticsAsync(CategoryStatisticsDto categoryStatisticsDto)
        {
            var categoryStatistics = new CategoryStatistics
            {
                QuestionsCountPerCategory = categoryStatisticsDto.QuestionsCountPerCategory
                    .Select(q => new CategoryQuantity
                    {
                        Category = q.Category,
                        Quantity = q.Quantity
                    }).ToList(),
                CardsCountPerCategory = categoryStatisticsDto.CardsCountPerCategory
                    .Select(c => new CategoryQuantity
                    {
                        Category = c.Category,
                        Quantity = c.Quantity
                    }).ToList(),
                QuestionsCountPlatform = categoryStatisticsDto.QuestionsCountPlatform,
                ExamsStarted = categoryStatisticsDto.ExamsStarted,
                OrdersCompleted = categoryStatisticsDto.OrdersCompleted,
                CardsCountPlatform = categoryStatisticsDto.CardsCountPlatform
            };
            await _categories.InsertOneAsync(categoryStatistics);
            return categoryStatistics;
        }

        public async Task<bool> UpdateCategoryStatisticsAsync(string id, CategoryStatisticsDto categoryStatisticsDto)
        {
            var categoryStatistics = new CategoryStatistics
            {
                Id = id,
                QuestionsCountPerCategory = categoryStatisticsDto.QuestionsCountPerCategory
                    .Select(q => new CategoryQuantity
                    {
                        Category = q.Category,
                        Quantity = q.Quantity
                    }).ToList(),
                UsersJoined = categoryStatisticsDto.UsersJoined,
                QuestionsCountPlatform = categoryStatisticsDto.QuestionsCountPlatform,
                ExamsStarted = categoryStatisticsDto.ExamsStarted,
                OrdersCompleted = categoryStatisticsDto.OrdersCompleted,
                ExamDate = categoryStatisticsDto.ExamDate,
                CardsCountPerCategory = categoryStatisticsDto.CardsCountPerCategory
                    .Select(c => new CategoryQuantity
                    {
                        Category = c.Category,
                        Quantity = c.Quantity
                    }).ToList(),
                CardsCountPlatform = categoryStatisticsDto.CardsCountPlatform
            };
            var result = await _categories.ReplaceOneAsync(stat => stat.Id == id, categoryStatistics);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteCategoryStatisticsAsync(string id)
        {
            var result = await _categories.DeleteOneAsync(stat => stat.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
