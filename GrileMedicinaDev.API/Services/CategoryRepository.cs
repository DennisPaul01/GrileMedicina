using GrileMedicinaDev.Entities;
using GrileMedicinaDev.Models;
using MongoDB.Driver;
using System.Threading.Tasks;
using GrileMedicinaDev.Data;

namespace GrileMedicinaDev.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoCollection<CategoryEntity> _categories;

        public CategoryRepository(MongoDbService mongoDbService)
        {
            _categories = mongoDbService.Database?.GetCollection<CategoryEntity>("categories");
        }

        public async Task<List<CategoryEntity>> GetCategoryAsync(string[] select, int limit, int? questionsCountPlatform, int? questionsCountPerCategory, int? cardsCountPlatform, int? cardsCountPerCategory, int? usersJoined, int? examsStarted, int? ordersCompleted, DateTime? examDate)
        {
            var filter = Builders<CategoryEntity>.Filter.Empty;
            var projection = Builders<CategoryEntity>.Projection.Include(select[0]);

            for (int i = 1; i < select.Length; i++)
            {
                projection = projection.Include(select[i]);
            }

            if (questionsCountPlatform.HasValue)
            {
                projection = projection.Include("questionsCountPlatform");
            }
            if (questionsCountPerCategory.HasValue)
            {
                projection = projection.Include("questionsCountPerCategory");
            }
            if (cardsCountPlatform.HasValue)
            {
                projection = projection.Include("cardsCountPlatform");
            }
            if (cardsCountPerCategory.HasValue)
            {
                projection = projection.Include("cardsCountPerCategory");
            }
            if (usersJoined.HasValue)
            {
                projection = projection.Include("usersJoined");
            }
            if (examsStarted.HasValue)
            {
                projection = projection.Include("examsStarted");
            }
            if (ordersCompleted.HasValue)
            {
                projection = projection.Include("ordersCompleted");
            }
            if (examDate.HasValue)
            {
                projection = projection.Include("examDate");
            }

            return await _categories.Find(filter).Project<CategoryEntity>(projection).Limit(limit).ToListAsync();
        }

        public async Task<CategoryEntity> CreateCategoryAsync(CategoryDto categoryDto)
        {
            var category = new CategoryEntity
            {
                Category = categoryDto.Category,
                Quantity = categoryDto.Quantity
            };
            await _categories.InsertOneAsync(category);
            return category;
        }

        public async Task<bool> UpdateCategoryAsync(string id, CategoryDto categoryDto)
        {
            var category = new CategoryEntity
            {
                Id = id,
                Category = categoryDto.Category,
                Quantity = categoryDto.Quantity
            };
            var result = await _categories.ReplaceOneAsync(stat => stat.Id == id, category);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteCategoryAsync(string id)
        {
            var result = await _categories.DeleteOneAsync(stat => stat.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<(bool, string)> CheckCategoriesExistAsync(string[] categoryNames)
        {
            var filter = Builders<CategoryEntity>.Filter.In(c => c.Category, categoryNames);
            var existingCategories = await _categories.Find(filter).ToListAsync();
            var existingCategoryNames = existingCategories.Select(c => c.Category).ToHashSet();

            foreach (var categoryName in categoryNames)
            {
                if (!existingCategoryNames.Contains(categoryName))
                {
                    return (false, categoryName);
                }
            }

            return (true, null);
        }
    }
}
