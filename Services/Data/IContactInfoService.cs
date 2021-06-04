using Data.Models;
using System.Threading.Tasks;

namespace Services.Data
{
    public interface IContactInfoService
    {
        Task<ContactInfo> Get();
        Task Update(ContactInfo biography);
    }
}
