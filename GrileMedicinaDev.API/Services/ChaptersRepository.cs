using GrileMedicinaDev.Entities;
using GrileMedicinaDev.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using GrileMedicinaDev.Data;

namespace GrileMedicinaDev.Services
{
    public class ChaptersRepository : IChaptersRepository
    {
        private readonly IMongoCollection<Chapter> _chapters;

        public ChaptersRepository(MongoDbService mongoDbService)
        {
            _chapters = mongoDbService.Database?.GetCollection<Chapter>("chapters");
        }

        public async Task<IEnumerable<Chapter>> GetAllChaptersAsync(
            string? name,
            string? createdBy,
            bool? isUserContent,
            bool? explanationsGenerating,
            List<string>? categories,
            int? quantity,
            List<string>? pages)
        {
            var filter = Builders<Chapter>.Filter.Empty;

            if (categories != null && categories.Count > 0)
            {
                filter = Builders<Chapter>.Filter.In("Categories", categories);
            }
            // If categories is null or empty, all chapters will be returned

            // Add additional filters based on other parameters if needed
            // Example:
            // if (!string.IsNullOrEmpty(name))
            // {
            //     filter = Builders<Chapter>.Filter.And(filter, Builders<Chapter>.Filter.Eq(x => x.Name, name));
            // }

            return await _chapters.Find(filter).ToListAsync();
        }

        public async Task<Chapter?> GetChapterByIdAsync(string id)
        {
            var filter = Builders<Chapter>.Filter.Eq(x => x.Id, id);
            return await _chapters.Find(filter).FirstOrDefaultAsync();
        }


        public async Task<Chapter> CreateChapterFromDtoAsync(ChapterForCreationDto chapterDto)
        {
            var chapter = new Chapter
            {
                Name = chapterDto.Name,
                IsUserContent = chapterDto.IsUserContent,
                Categories = chapterDto.Categories,
                Quantity = 0,
                CreatedBy = chapterDto.CreatedBy,
                Pages = new Pages
                {

                    Start = chapterDto.Pages.Start,
                    End = chapterDto.Pages.End,

                },
                ExplanationsGenerating = chapterDto.ExplanationsGenerating
            };
            await _chapters.InsertOneAsync(chapter);
            return chapter;
        }
        public async Task<bool> UpdateChapterAsync(string id, Chapter chapter)
        {
            var filter = Builders<Chapter>.Filter.Eq(x => x.Id, id);
            var update = Builders<Chapter>.Update
                // ...map properties from chapter to update definition...
                .Set(x => x.Name, chapter.Name); // Example property
            var result = await _chapters.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }
        public async Task<bool> DeleteChapterAsync(string id)
        {
            var filter = Builders<Chapter>.Filter.Eq(x => x.Id, id);
            var result = await _chapters.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }

        public async Task<bool> DoChaptersExistAsync(IEnumerable<string> chapterIds)
        {
            var filter = Builders<Chapter>.Filter.In(x => x.Id, chapterIds);
            var count = await _chapters.CountDocumentsAsync(filter);
            return count == chapterIds.Count();
        }
        public async Task<bool> UpdateQuantityChapterAsync(string id, int quantity)
        {
            var filter = Builders<Chapter>.Filter.Eq(x => x.Id, id);
            var update = Builders<Chapter>.Update
                .Set(x => x.Quantity, quantity);
            var result = await _chapters.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }
    }
}
