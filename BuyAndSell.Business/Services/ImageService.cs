using BuySell.Business.Services.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Business.Services
{
    public class ImageService : IImageService
    {
        public async Task<string?> UploadImageAsync(IFormFile? image)
        {
            if (image is null)
                return null;
            var fileName = await UploadFileLocal(image);
            return fileName;
        }

        public async Task<byte[]> GetImageAsync(string fileName)
        {
            return await File.ReadAllBytesAsync(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "NAS", fileName));
        }

        public async Task<List<string>> UploadImagesAsync(List<IFormFile> images)
        {
            var imageUrls = new List<string>();
            foreach (var image in images)
            {
                var fileName = await UploadFileLocal(image);
                imageUrls.Add(fileName);
            }

            return imageUrls;
        }

        private static async Task<string> UploadFileLocal(IFormFile image)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "NAS");
            var guid = Guid.NewGuid().ToString();
            path = Path.Combine(path, guid);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var imagePath = Path.Combine(guid, image.FileName);
            path = Path.Combine(path, image.FileName);

            await using var inputStream = new FileStream(path, FileMode.Create);
            await image.CopyToAsync(inputStream);
            inputStream.Close();

            return imagePath;
        }
    }
}
