using BuySell.Business.Services.Contracts;
using BuySell.Host.Validators.Images;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuySell.Host.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ImagesController : ControllerBase
{
    private readonly IImageService _imageService;

    public ImagesController(IImageService imageService, IConfiguration configuration, IWebHostEnvironment environment)
    {
        _imageService = imageService;
    }

    [HttpPost]
    public async Task<IActionResult> UploadImages([FromForm] List<IFormFile> images)
    {
        var result = await new ImagesValidator().ValidateAsync(images);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }

        var imageUrls = await _imageService.UploadImagesAsync(images);
        return Ok(imageUrls);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetImage(string? imagePath)
    {
        if (imagePath is null)
            return NotFound();
        var bytes = await _imageService.GetImageAsync(imagePath);
        var extension = imagePath.Split('.').Last();
        return File(bytes, $"image/{extension}");
    }
}