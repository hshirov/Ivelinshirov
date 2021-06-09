using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ivelinshirov.Models
{
    public class AddSlideshowItemModel
    {
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        [DisplayName("Upload image file")]
        [Required(ErrorMessage = "An image is required.")]
        public IFormFile ImageFile { get; set; }
    }
}
