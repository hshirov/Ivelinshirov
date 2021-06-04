using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Services.Data
{
    public class ContactInfoService : IContactInfoService
    {
        private readonly ApplicationDbContext _context;

        public ContactInfoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ContactInfo> Get()
        {
            return await _context.ContactInfo.FirstOrDefaultAsync();
        }

        public async Task Update(ContactInfo contactInfo)
        {
            var contactInfoToUpdate = await Get();
            _context.Entry(contactInfoToUpdate).CurrentValues.SetValues(contactInfo);

            await _context.SaveChangesAsync();
        }
    }
}
