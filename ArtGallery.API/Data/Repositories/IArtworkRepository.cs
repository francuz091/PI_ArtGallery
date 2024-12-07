using ArtGallery.API.Models.Domain;

namespace ArtGallery.API.Data.Repositories
{
    public interface IArtworkRepository
    {
        Task<Artwork> CreateAsync(Artwork artwork);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<Artwork>> GetAllAsync();
        Task<Artwork> GetByIdAsync(int id);
        Task UpdateAsync(Artwork artwork);
        Task DeleteAsync(int id);
    }
}
