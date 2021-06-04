using Data.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Ivelinshirov.Common
{
    public static class ImageFileHelper
    {
        public static async Task SaveImageFromArtwork(Artwork artwork, IWebHostEnvironment _hostEnvironment)
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

        public static void DeleteArtworkImage(Artwork artwork)
        {
            File.Delete(artwork.ImagePath);
        }
    }
}
