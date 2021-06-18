using Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests.Common
{
    public static class DataSeeder
    {
        public static async Task SeedDataInDbContext<T>(ApplicationDbContext context, IEnumerable<T> data)
        {
            foreach(var entity in data)
            {
                await context.AddAsync(entity);
            }
            await context.SaveChangesAsync();
        }
    }
}
