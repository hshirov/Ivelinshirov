using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Data
{
    public interface ICategoryService
    {
        public Task Add(Category category);
        public Task Remove(int id);
        public Task<Category> GetById(int id);
        public Task<Category> GetByName(string name);
        public Task<IEnumerable<Category>> GetAll();
    }
}
