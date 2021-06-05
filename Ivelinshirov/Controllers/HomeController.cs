using Ivelinshirov.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Data;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Ivelinshirov.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArtworkService _artworkService;

        public HomeController(IArtworkService artworkService)
        {
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

        [Route("/Home/Error/{code:int}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int code)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, StatusCode = code });
        }
    }
}
