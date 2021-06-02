using Ivelinshirov.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.External;
using System;
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
                if (Request.Cookies["LimitCookie"] != null)
                {
                    ModelState.AddModelError("Content", "You have reached your email limit. Try again later.");

                    return View(model);
                }

                await _messageService.SendEmailAsync(model.Name, model.Email, model.Subject, model.Content);

                // Set an email limiter for 1 hour

                CookieOptions cookie = new CookieOptions();
                cookie.Expires = DateTime.Now.AddHours(1);
                Response.Cookies.Append("LimitCookie", "LimitCookie", cookie);

                TempData["Success"] = true;

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
