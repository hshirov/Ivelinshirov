using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Data
{
    public class ArtworkService : IArtworkService
    {
        private readonly ApplicationDbContext _context;

        public ArtworkService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Artwork artwork)
        {
            await _context.AddAsync(artwork);
            await _context.SaveChangesAsync();

            await AssignPositionPreference(artwork);
        }

        public async Task<Artwork> Get(int id)
        {
            return await GetAll()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Artwork> GetAll()
        {
            return _context.Artworks.Include(x => x.Category).AsQueryable();
        }

        public async Task<IEnumerable<Artwork>> GetAllFeaturedOnHomePage()
        {
            return await GetAll().Where(x => x.IsFeaturedOnHomePage).OrderBy(x => x.PositionPreference).ToListAsync();
        }

        public async Task<IEnumerable<Artwork>> GetAllFromCategory(int categoryId)
        {
            return await GetAll().Where(x => x.Category.Id == categoryId && !x.IsFeaturedOnHomePage).OrderBy(x => x.PositionPreference).ToListAsync();
        }

        public async Task Remove(int id)
        {
            var artwork = await Get(id);

            if(artwork != null)
            {
                _context.Artworks.Remove(artwork);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Update(Artwork artwork)
        {
            Artwork artworkToUpdate = await Get(artwork.Id);
            _context.Entry(artworkToUpdate).CurrentValues.SetValues(artwork);

            await _context.SaveChangesAsync();
        }

        public async Task UpdatePosition(int id, int newPosition)
        {
            var artworkToUpdate = await Get(id);
            if (artworkToUpdate != null)
            {
                artworkToUpdate.PositionPreference = newPosition;
            }

            _context.Artworks.Update(artworkToUpdate);
            await _context.SaveChangesAsync();
        }

        private async Task AssignPositionPreference(Artwork artwork)
        {
            int artworkId = artwork.Id;
            await UpdatePosition(artworkId, artworkId);
        }
    }
}
