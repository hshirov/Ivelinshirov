using Microsoft.AspNetCore.Mvc;

namespace Ivelinshirov.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
