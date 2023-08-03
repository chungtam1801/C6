using Microsoft.AspNetCore.Components.Forms;
using System.Security.AccessControl;

namespace BlazorServerApp.Pages
{
    public partial class UploadFile
    {
        IReadOnlyList<IBrowserFile> files;
        List<string> urls = new List<string>();
        string dropClass = string.Empty;
        const int maxFileSize = 10485760;
        private async Task<string> SaveFile(IBrowserFile file,string guid = null)
        {
            if(guid == null)
            {
                guid = Guid.NewGuid().ToString();
            }
            var relativePath = Path.Combine("uploads", guid);
            var dirToSave =Path.Combine(_env.WebRootPath, relativePath);
            var dir = new DirectoryInfo(dirToSave);
            if (!dir.Exists)
            {
                dir.Create();
            }
            var filePath = Path.Combine(dirToSave, file.Name);
            using(var stream = file.OpenReadStream(maxFileSize))
            {
                using(var mstream = new MemoryStream())
                {
                    await stream.CopyToAsync(mstream);
                    await File.WriteAllBytesAsync(filePath,mstream.ToArray());
                }              
            }
            var url = Path.Combine(relativePath, file.Name).Replace("\\", "/");
            return url;
        }
        private async Task<List<string>> SaveFiles(IReadOnlyList<IBrowserFile> files)
        {
            var list = new List<string>();
            var guid = Guid.NewGuid().ToString();
            foreach(var file in files)
            {
                var url = await SaveFile(file,guid);
                list.Add(url);
            }
            return list;
        }
        private void HandleDragEnter()
        {
            dropClass = "dropAreaDrug";
        }
        private void HandleDragLeave()
        {
            dropClass = string.Empty;
        }
        private async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            dropClass = string.Empty;
            try
            {
                if(e.FileCount>1)
                {
                    files = e.GetMultipleFiles();
                    urls.Clear();
                    urls.AddRange(await SaveFiles(files));
                }
                else
                {
                    files = null;
                    var url = await SaveFile(e.File);
                    urls.Clear();
                    urls.Add(url);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
