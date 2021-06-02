using Ivelinshirov.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Data;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Ivelinshirov.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IArtworkService _artworkService;

        public HomeController(ILogger<HomeController> logger, ICategoryService categoryService, IArtworkService artworkService)
        {
            _logger = logger;
            _categoryService = categoryService;
            _artworkService = artworkService;
        }

        public async Task<IActionResult> Index()
        {
            var featuredArtwork = await _artworkService.GetAllFeaturedOnHomePage();

            return View(featuredArtwork);
        }

        [Route("/Login")]
        public IActionResult Login()
        {
            return Redirect("/Identity/Account/Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
