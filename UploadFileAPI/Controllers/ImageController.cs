using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace UploadFileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IHostEnvironment _env;
        public ImageController(IHostEnvironment environment)
        {
            this._env = environment;
        }
        [HttpPost]
        public async Task<IActionResult> PostImage(IFormFile image)
        {
            if (image == null || image.Length == 0) return BadRequest("Upload a file");
            string fileName = image.FileName;
            string extension = Path.GetExtension(fileName);
            string[] allowedExtension = { ".jpg", ".png", ".bmp" };
            if (!allowedExtension.Contains(extension)) return BadRequest("File is not valid");
            string newFileName = $"{Guid.NewGuid()}{extension}";
            string filePath = Path.Combine(_env.ContentRootPath, "wwwroot", "Images", newFileName);
            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await image.CopyToAsync(stream);
            }
            return Ok($"Images/{newFileName}");
        }
    }
}
