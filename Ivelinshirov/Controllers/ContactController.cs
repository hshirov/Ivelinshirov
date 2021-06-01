using Ivelinshirov.Models;
using Microsoft.AspNetCore.Mvc;
using Services.External;
using System.Threading.Tasks;

namespace Ivelinshirov.Controllers
{
    public class ContactController : Controller
    {
        private readonly IMessageService _messageService;

        public ContactController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public IActionResult Index()
        {
            ViewBag.Success = TempData["Success"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                await _messageService.SendEmailAsync(model.Name, model.Email, model.Subject, model.Content);

                TempData["Success"] = true;
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
