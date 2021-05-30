using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Services.Data
{
    public class BiographyService : IBiographyService
    {
        private readonly ApplicationDbContext _context;

        public BiographyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Biography> Get()
        {
            return await _context.Biography.FirstOrDefaultAsync();
        }

        public async Task Update(Biography biography)
        {
            var biographyToUpdate = await Get();
            _context.Entry(biographyToUpdate).CurrentValues.SetValues(biography);

            await _context.SaveChangesAsync();

        }
    }
}
