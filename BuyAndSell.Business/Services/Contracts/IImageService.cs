using Microsoft.AspNetCore.Http;

namespace BuySell.Business.Services.Contracts
{
    public interface IImageService
    {
        Task<byte[]> GetImageAsync(string fileName);
        Task<string?> UploadImageAsync(IFormFile? image);
        Task<List<string>> UploadImagesAsync(IFormFile image);
    }
}