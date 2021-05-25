﻿using Ivelinshirov.Models;
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

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Art(string id)
        {
            var category = await _categoryService.GetByName(id);

            if (category != null)
            {
                var artwork = await _artworkService.GetAllFromCategory(category.Id);

                if(artwork != null)
                {
                    return View(artwork);
                }
            }

            return NotFound();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
