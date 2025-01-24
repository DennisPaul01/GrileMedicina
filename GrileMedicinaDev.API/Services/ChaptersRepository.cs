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

        public async Task<IEnumerable<Chapter>> GetAllChaptersAsync()
        {
            return await _chapters.Find(_ => true).ToListAsync();
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
                // ...map properties from chapterDto to Chapter...
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
    }
}
