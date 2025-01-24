using GrileMedicinaDev.Entities;
using GrileMedicinaDev.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrileMedicinaDev.Services
{
    public interface IChaptersRepository
    {
        Task<IEnumerable<Chapter>> GetAllChaptersAsync();
        Task<Chapter?> GetChapterByIdAsync(string id);
        Task<Chapter> CreateChapterFromDtoAsync(ChapterForCreationDto chapterDto);
        Task<bool> UpdateChapterAsync(string id, Chapter chapter);
        Task<bool> DeleteChapterAsync(string id);
    }
}
