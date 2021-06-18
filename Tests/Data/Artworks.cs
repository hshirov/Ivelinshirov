using Data.Models;

namespace Tests.Data
{
    public static class Artworks
    {
        public static readonly Artwork Painting1 = new Artwork
        {
            Id = 1,
            Title = "Painting 1",
            ImageName = "painting1",
            IsFeaturedOnHomePage = true,
            PositionPreference = 1,
            Category = Categories.Paintings
        };

        public static readonly Artwork Painting2 = new Artwork
        {
            Id = 2,
            Title = "Painting 2",
            ImageName = "painting2",
            IsFeaturedOnHomePage = false,
            PositionPreference = 2,
            Category = Categories.Paintings
        };

        public static readonly Artwork Drawing1 = new Artwork
        {
            Id = 3,
            Title = "Drawing 1",
            ImageName = "drawing1",
            IsFeaturedOnHomePage = false,
            PositionPreference = 1,
            Category = Categories.Drawings
        };
    }
}
