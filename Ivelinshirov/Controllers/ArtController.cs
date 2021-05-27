using Microsoft.AspNetCore.Mvc;
using Services.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Ivelinshirov.Controllers
{
    public class ArtController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IArtworkService _artworkService;

        public ArtController(ICategoryService categoryService, IArtworkService artworkService)
        {
            _categoryService = categoryService;
            _artworkService = artworkService;
        }

        [Route("Art/{id?}")]
        public async Task<IActionResult> Index(string id)
        {
            var category = await _categoryService.GetByName(id);

            if (category != null)
            {
                var artwork = await _artworkService.GetAllFromCategory(category.Id);

                if (artwork.Any())
                {
                    return View(artwork);
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
