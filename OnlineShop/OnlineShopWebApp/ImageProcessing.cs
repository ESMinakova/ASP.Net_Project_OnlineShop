using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OnlineShopWebApp.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopWebApp
{
    public class ImageProcessing
    {        
        private IWebHostEnvironment appEnvironment;

        public ImageProcessing(IWebHostEnvironment appEnvironment)
        {            
            this.appEnvironment = appEnvironment;
        }
        public async Task<string> UploadFileAsync(IFormFile uploadedFile, ImageFolders folder )
        {    
            if (uploadedFile != null)
            {
                string folderPath = Path.Combine(appEnvironment.WebRootPath + "/Content/" + folder);
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                var fileName = Guid.NewGuid() + "." + uploadedFile.FileName.Split('.').Last();
                var path = Path.Combine(folderPath, fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                    await uploadedFile.CopyToAsync(fileStream);
                return "/Content/" + folder + "/" + fileName;
            }
            return null;
                      
        }

        public async Task<List<string>> UploadFilesAsync(List<IFormFile> uploadedFiles, ImageFolders folder)
        {
            var imagesPaths = new List<string>();
            foreach (var file in uploadedFiles)
            {
                var path = await UploadFileAsync(file, folder);
                imagesPaths.Add(path);
            }
            return imagesPaths;
        }
    }
}
