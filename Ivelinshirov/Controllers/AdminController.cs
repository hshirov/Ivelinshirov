using Data.Models;
using Ivelinshirov.Common;
using Ivelinshirov.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Services.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Ivelinshirov.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IArtworkService _artworkService;
        private readonly ICategoryService _categoryService;
        private readonly IBiographyService _biographyService;
        private readonly IContactInfoService _contactInfoService;

        public AdminController(
            IWebHostEnvironment hostEnvironment, 
            IArtworkService artworkService, 
            ICategoryService categoryService, 
            IBiographyService biographyService,
            IContactInfoService contactInfoService
        )
        {
            _hostEnvironment = hostEnvironment;
            _artworkService = artworkService;
            _categoryService = categoryService;
            _biographyService = biographyService;
            _contactInfoService = contactInfoService;
        }

        public async Task<IActionResult> Index(string id)
        {
            if(id == null)
            {
                var defaultCategory = _categoryService.GetAll().Result.FirstOrDefault();

                if(defaultCategory != null)
                {
                    id = defaultCategory.Name;
                }
                else
                {
                    return RedirectToAction(nameof(this.Categories));
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

            return StatusCode(404);
        }

        public async Task<IActionResult> AddArtwork(string category)
        {
            AddArtworkModel model = new AddArtworkModel()
            {
                AllCategories = await _categoryService.GetAll()
            };

            var defaultCategory = await _categoryService.GetByName(category);

            if(defaultCategory != null)
            {
                model.CategoryId = defaultCategory.Id;
                model.RefererCategoryName = defaultCategory.Name;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddArtwork(AddArtworkModel model)
        {
            if (ModelState.IsValid)
            {
                Artwork artwork = new Artwork()
                {
                    Title = model.Title,
                    Category = await _categoryService.GetById(model.CategoryId),
                    ImageFile = model.ImageFile,
                    IsFeaturedOnHomePage = model.IsFeaturedOnHomePage
                };

                await ImageFileHelper.SaveImageFromArtwork(artwork, _hostEnvironment);
                await _artworkService.Add(artwork);

                return RedirectToAction(nameof(this.Index), new { id = artwork.Category.Name });
            }

            model.AllCategories = await _categoryService.GetAll();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveArtwork(int id)
        {
            ImageFileHelper.DeleteArtworkImage(await _artworkService.Get(id));
            await _artworkService.Remove(id);

            string referer = Request.Headers["Referer"].ToString();
            return Redirect(referer);
        }

        public async Task<IActionResult> EditArtwork(int id)
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

            return StatusCode(500);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditArtwork(EditArtworkModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _artworkService.Update(viewModel.Model);

                return RedirectToAction(nameof(this.Index) , new { id = viewModel.Referer });
            }

            return View(viewModel);
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

                return RedirectToAction(nameof(this.Categories));
            }

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            var category = await _categoryService.GetById(id);

            if (category != null)
            {
                // Remove image files
                foreach (var artwork in category.Artworks)
                {
                    ImageFileHelper.DeleteArtworkImage(artwork);
                }

                await _categoryService.Remove(id);
            }

            return RedirectToAction(nameof(this.Categories));
        }

        public async Task<IActionResult> About()
        {
            Biography bio = await _biographyService.Get();
            ViewBag.Success = TempData["Success"];

            return View(bio);
        }

        [HttpPost]
        public async Task<IActionResult> EditAbout(Biography biography)
        {
            if (ModelState.IsValid)
            {
                await _biographyService.Update(biography);

                TempData["Success"] = true;

                return RedirectToAction(nameof(this.About));
            }

            return StatusCode(500);
        }

        public async Task<IActionResult> Contact()
        {
            var contactInfo = await _contactInfoService.Get();
            ViewBag.SuccessContact = TempData["SuccessContact"];

            return View(contactInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactInfo model)
        {
            if (ModelState.IsValid)
            {
                await _contactInfoService.Update(model);

                TempData["SuccessContact"] = true;
                return RedirectToAction(nameof(this.Contact));
            }

            return View(model);
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
    }
}