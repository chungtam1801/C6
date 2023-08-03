using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UploadFileMVC.Controllers
{
    public class UploadFileController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IHostingEnvironment Environment;
        public UploadFileController(IHostingEnvironment environment)
        {
            _httpClient = new HttpClient();
            Environment = environment;
        }
        public IActionResult ShowPageUploadFile()
        {
            return View();
        }
        public async Task<IActionResult> UploadFile(IFormFile files)
        {
            string fileName;
            var extension = "." + files.FileName.Split('.')[files.FileName.Split('.').Length - 1];
            fileName = DateTime.Now.Ticks + extension;
            string path = Path.Combine(this.Environment.WebRootPath, "Files/", fileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await files.CopyToAsync(stream);
            }     
            return RedirectToAction("ShowPageDownloadFile", "DownloadFile");
            //var value = JsonConvert.SerializeObject(files);
            //var response = await _httpClient.PostAsync("https://localhost:7021/api/UploadFile/UploadFileToDatabase", new StringContent(value, Encoding.UTF8, "application/json"));
            //if (response.IsSuccessStatusCode)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            //else return BadRequest();
        }
    }
}
