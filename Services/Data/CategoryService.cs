using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Data
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Category category)
        {
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
           return await _context.Categories
                .Include(x => x.Artworks)
                .ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {
            return await _context.Categories
                .Include(x => x.Artworks)
                .AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category> GetByName(string name)
        {
            return await _context.Categories
                .Include(x => x.Artworks)
                .AsQueryable().FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task Remove(int id)
        {
            var categoryToRemove = await GetById(id);
            if (categoryToRemove.Artworks != null && categoryToRemove.Artworks.Any())
            {
                foreach(var artwork in categoryToRemove.Artworks)
                {
                    //await _artworkService.Remove(artwork.Id);
                    _context.Artworks.RemoveRange(categoryToRemove.Artworks);
                    await _context.SaveChangesAsync();
                }
            }

            _context.Categories.Remove(categoryToRemove);
            await _context.SaveChangesAsync();
        }
    }
}
