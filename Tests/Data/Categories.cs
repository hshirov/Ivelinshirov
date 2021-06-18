using Data.Models;
using System.Collections.Generic;

namespace Tests.Data
{
    public static class Categories
    {
        public static readonly Category Paintings = new Category
        {
            Id = 1,
            Name = "Paintings",
            Artworks = new List<Artwork>()
        };

        public static readonly Category Drawings = new Category
        {
            Id = 2,
            Name = "Drawings",
            Artworks = new List<Artwork>()
        };
    }
}
