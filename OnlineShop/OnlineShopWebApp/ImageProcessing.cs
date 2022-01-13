using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OnlineShopWebApp.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OnlineShopWebApp
{
    public class ImageProcessing
    {        
        private IWebHostEnvironment appEnvironment;

        public ImageProcessing(IWebHostEnvironment appEnvironment)
        {            
            this.appEnvironment = appEnvironment;
        }
        public string UploadFile(IFormFile uploadedFile, ImageFolders folder )
        {    
            if (uploadedFile != null)
            {
                string folderPath = Path.Combine(appEnvironment.WebRootPath + "/Content/" + folder);
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                var fileName = Guid.NewGuid() + "." + uploadedFile.FileName.Split('.').Last();
                var path = Path.Combine(folderPath, fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                    uploadedFile.CopyToAsync(fileStream).Wait();
                return "/Content/" + folder + "/" + fileName;
            }
            return null;
                      
        }

        public List<string> UploadFiles(List<IFormFile> uploadedFiles, ImageFolders folder)
        {
            var imagesPaths = new List<string>();
            foreach (var file in uploadedFiles)
            {
                imagesPaths.Add(UploadFile(file, folder));
            }
            return imagesPaths;
        }
    }
}
