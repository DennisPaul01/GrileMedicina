using GrileMedicinaDev.Entities;
using GrileMedicinaDev.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrileMedicinaDev.Services
{
    public interface IChaptersRepository
    {
        Task<IEnumerable<Chapter>> GetAllChaptersAsync(
            string? name,
            string? createdBy,
            bool? isUserContent,
            bool? explanationsGenerating,
            List<string>? categories,
            int? quantity,
            List<string>? pages);
        Task<Chapter?> GetChapterByIdAsync(string id);
        Task<Chapter> CreateChapterFromDtoAsync(ChapterForCreationDto chapterDto);
        Task<bool> UpdateChapterAsync(string id, Chapter chapter);
        Task<bool> DeleteChapterAsync(string id);
    }
}
