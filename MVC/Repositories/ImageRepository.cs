using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MVC.Interfaces;
using System;
using System.IO;

namespace MVC.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageRepository(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public void DeleteImage(string path)
        {
            if (path != "2.jpg")
            {
                var oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", path);
                if (File.Exists(oldPath))
                {
                    File.Delete(oldPath);
                }
            }
        }

        public string UpdateImage(IFormFile Image, string oldPath)
        {
           DeleteImage(oldPath);
           var result= UploadImage(Image);
           return result;
        }

        public string UploadImage(IFormFile Image)
        {
            if(Image == null)
            {
                return "2.jpg";
            }
            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
            var ImageName = Guid.NewGuid().ToString() + "_" + Image.FileName;
            var ImagePath=Path.Combine(uploadPath, ImageName);
            using var fileStream = new FileStream(ImagePath, FileMode.Create);
            Image.CopyTo(fileStream);
            return ImageName;
        }
    }
}
