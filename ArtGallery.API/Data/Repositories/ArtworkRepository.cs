using ArtGallery.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.API.Data.Repositories
{
    public class ArtworkRepository : IArtworkRepository
    {
        private readonly ArtGalleryContext _context;
        private readonly ILogger<ArtworkRepository> _logger;

        public ArtworkRepository(ArtGalleryContext context, ILogger<ArtworkRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Artwork> CreateAsync(Artwork artwork)
        {
            try
            {
                await _context.Artworks.AddAsync(artwork);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Artwork with ID {artwork.IDArtWork} created successfully");
                return artwork;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating artwork");
                throw;
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Artworks.AnyAsync(a => a.IDArtWork == id);
        }

        public async Task<IEnumerable<Artwork>> GetAllAsync()
        {
            try
            {
                return await _context.Artworks
                    .Include(a => a.ArtWorkType)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all artworks");
                throw;
            }
        }

        public async Task<Artwork> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Artworks
                    .Include(a => a.ArtWorkType)
                    .FirstOrDefaultAsync(a => a.IDArtWork == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving artwork with ID {id}");
                throw;
            }
        }

        public async Task UpdateAsync(Artwork artwork)
        {
            try
            {
                _context.Entry(artwork).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Artwork with ID {artwork.IDArtWork} updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating artwork with ID {artwork.IDArtWork}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var artwork = await _context.Artworks.FindAsync(id);
                if (artwork != null)
                {
                    _context.Artworks.Remove(artwork);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Artwork with ID {id} deleted successfully");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting artwork with ID {id}");
                throw;
            }
        }
    }
}
