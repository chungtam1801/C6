using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using UploadFileMVC.Models;

namespace UploadFileMVC.Controllers
{
    public class DownloadFileController : Controller
    {
        private IHostingEnvironment Environment;
        public DownloadFileController(IHostingEnvironment _environment)
        {
            Environment = _environment; 
        }
        public IActionResult ShowPageDownloadFile()
        {
            string[] filePaths = Directory.GetFiles(Path.Combine(this.Environment.WebRootPath, "Files/"));
            List<FileModel> files = new List<FileModel>();
            foreach(string filePath in filePaths)
            {
                files.Add(new FileModel() { FileName = Path.GetFileName(filePath) });
            }
            return View(files);
        }
        public FileResult DownloadFile(string fileName)
        {
            string path = Path.Combine(this.Environment.WebRootPath, "Files/",fileName);
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "application/octet-stream", fileName);
        }
    }
}
