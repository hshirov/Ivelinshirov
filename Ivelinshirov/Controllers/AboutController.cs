using Microsoft.AspNetCore.Mvc;
using Services.Data;
using System.Threading.Tasks;

namespace Ivelinshirov.Controllers
{
    public class AboutController : Controller
    {
        private readonly IBiographyService _biographyService;
        public AboutController(IBiographyService biographyService)
        {
            _biographyService = biographyService;
        }

        public async Task<IActionResult> Index()
        {
            var bio = await _biographyService.Get();

            if (bio != null)
            {
                return View(bio);
            }

            return StatusCode(404);
        }
    }
}
