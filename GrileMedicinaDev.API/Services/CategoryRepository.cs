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

        public async Task<List<CategoryEntity>> GetCategoryAsync()
        {
            return await _categories.Find(FilterDefinition<CategoryEntity>.Empty).ToListAsync();
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
    }
}
