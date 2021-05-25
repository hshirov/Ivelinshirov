using Data.Models;
using Ivelinshirov.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Services.Data;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ivelinshirov.Controllers
{
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IArtworkService _artworkService;
        private readonly ICategoryService _categoryService;

        public AdminController(IWebHostEnvironment hostEnvironment, IArtworkService artworkService, ICategoryService categoryService)
        {
            _hostEnvironment = hostEnvironment;
            _artworkService = artworkService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(string id)
        {
            if(id == null)
            {
                var defaultCategory = await _categoryService.GetById(1);

                if(defaultCategory != null)
                {
                    id = defaultCategory.Name;
                }
            }

            if (id != null)
            {
                var category = await _categoryService.GetByName(id);

                if (category != null) 
                { 
                    var artwork = await _artworkService.GetAllFromCategory(category.Id);

                    var artworkIndexModel = artwork.Select(e => new AdminArtworkIndexModel 
                    { 
                        Id = e.Id,
                        ImageName = e.ImageName
                    });

                    var model = new AdminArtworkModel()
                    {
                        Artworks = artworkIndexModel,
                        Category = category.Name
                    };

                    return View(model);
                }
            }

            return NotFound();
        }

        public async Task<IActionResult> Categories()
        {
            var categories = await _categoryService.GetAll();

            return View(categories);
        }

        public IActionResult AddCategory()
        {
            var emptyModel = new Category();

            return View(emptyModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.Add(category);

                return RedirectToAction("Categories");
            }

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            await _categoryService.Remove(id);

            // Remove image files

            return RedirectToAction("Categories");
        }

        public async Task<IActionResult> Add(string category)
        {
            AddArtworkModel model = new AddArtworkModel()
            {
                AllCategories = await _categoryService.GetAll()
            };

            var defaultCategory = await _categoryService.GetByName(category);

            if(defaultCategory != null)
            {
                model.CategoryId = defaultCategory.Id;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddArtworkModel model)
        {
            if (ModelState.IsValid)
            {
                Artwork artwork = new Artwork()
                {
                    Title = model.Title,
                    Category = await _categoryService.GetById(model.CategoryId),
                    ImageFile = model.ImageFile
                };

                await SaveImageFromArtwork(artwork);
                await _artworkService.Add(artwork);

                return RedirectToAction("Index", new { id = artwork.Category.Name });
            }

            model.AllCategories = await _categoryService.GetAll();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            DeleteArtworkImage(await _artworkService.Get(id));
            await _artworkService.Remove(id);

            string referer = Request.Headers["Referer"].ToString();
            return Redirect(referer);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var artwork = await _artworkService.Get(id);

            if(artwork != null)
            {
                var model = new EditArtworkModel()
                {
                    Model = artwork
                };

                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditArtworkModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _artworkService.Update(viewModel.Model);

                return RedirectToAction("Index" , new { id = viewModel.Referer });
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrder(UpdateOrderModel model)
        {
            for (int i = 0; i < model.Ids.Length; i++)
            {
                await _artworkService.UpdatePosition(model.Ids[i], i);
            }

            string referer = Request.Headers["Referer"].ToString();
            return Redirect(referer);
        }

        private async Task SaveImageFromArtwork(Artwork artwork)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileExtension = Path.GetExtension(artwork.ImageFile.FileName);
            string fileName = Path.GetFileNameWithoutExtension(artwork.ImageFile.FileName) + DateTime.Now.ToString("yymmssffff") + fileExtension;
            artwork.ImageName = fileName;

            string path = artwork.ImagePath = Path.Combine(wwwRootPath + "/images/artwork/", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await artwork.ImageFile.CopyToAsync(fileStream);
            }
        }

        private void DeleteArtworkImage(Artwork artwork)
        {
            System.IO.File.Delete(artwork.ImagePath);
        }
    }
}
