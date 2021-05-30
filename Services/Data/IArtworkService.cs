using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Services.Data
{
    public interface IArtworkService
    {
        public Task Add(Artwork artwork);
        public Task Remove(int id);
        public Task Update(Artwork artwork);
        public Task UpdatePosition(int id, int newPosition);
        public Task<Artwork> Get(int id);
        public IQueryable<Artwork> GetAll();
        public Task<IEnumerable<Artwork>> GetAllFeaturedOnHomePage();
        public Task<IEnumerable<Artwork>> GetAllFromCategory(int categoryId);
    }
}
