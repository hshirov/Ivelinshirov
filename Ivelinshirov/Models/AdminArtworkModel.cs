using System.Collections.Generic;

namespace Ivelinshirov.Models
{
    public class AdminArtworkModel
    {
        public IEnumerable<AdminArtworkIndexModel> Artworks { get; set; }
        public string Category { get; set; }
    }
}
