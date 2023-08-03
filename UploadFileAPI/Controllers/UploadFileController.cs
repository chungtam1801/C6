using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UploadFileAPI.Models;

namespace UploadFileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        private DemoDatabaseContext _context;
        public UploadFileController()
        {
                _context = new DemoDatabaseContext();
        }
        [HttpPost]
        private async Task<bool> UploadFileToSystemFile(IFormFile file)
        {
            bool isSaveSuccess = false;
            string fileName;
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = DateTime.Now.Ticks + extension;
                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files");
                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                isSaveSuccess = true;
            }
            catch
            {
                return false;
            }
            return isSaveSuccess;
        }
        [HttpPost]
        private bool UploadFileToDatabase(IFormFile files)
        {
            bool isSaveSuccess = false;
            try
            {
                if (files == null)
                {
                    if (files.Length > 0)
                    {
                        var fileName = Path.GetFileName(files.FileName);
                        var fileExtension = Path.GetExtension(fileName);
                        var newFileName = string.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);
                        var objFile = new Files
                        {
                            DocumentId = 0,
                            Name = newFileName,
                            FileType = fileExtension,
                            CreatedOn = DateTime.Now
                        };
                        using(var target = new MemoryStream())
                        {
                            files.CopyTo(target);
                            objFile.DataFiles = target.ToArray();
                        }
                        _context.Files.Add(objFile);
                        _context.SaveChanges();
                    }
                }
                isSaveSuccess = true;
            }
            catch
            {
                return false;
            }
            return isSaveSuccess;
        }
    }
}
