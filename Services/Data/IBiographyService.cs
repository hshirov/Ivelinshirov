using Data.Models;
using System.Threading.Tasks;

namespace Services.Data
{
    public interface IBiographyService
    {
        Task<Biography> Get();
        Task Update(Biography biography);
    }
}
