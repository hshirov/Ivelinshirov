﻿using Data.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ivelinshirov.Models
{
    public class AddArtworkModel
    {
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        [DisplayName("Upload image file")]
        [Required(ErrorMessage = "An image is required.")]
        public IFormFile ImageFile { get; set; }
        [Required(ErrorMessage = "A category is required.")]
        public int CategoryId { get; set; }
        public string RefererCategoryName { get; set; }
        public IEnumerable<Category> AllCategories { get; set; }
    }
}
