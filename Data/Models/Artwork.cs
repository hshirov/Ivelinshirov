using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Artwork
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public int PositionPreference { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public Category Category { get; set; }
    }
}
