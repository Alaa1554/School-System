using Microsoft.AspNetCore.Http;

namespace MVC.Interfaces
{
    public interface IImageRepository
    {
        string UploadImage(IFormFile Image);
        void DeleteImage(string path);
        string UpdateImage(IFormFile Image,string oldPath);

    }
}
