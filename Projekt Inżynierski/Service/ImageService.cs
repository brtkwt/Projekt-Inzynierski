using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projekt_Inżynierski.Interfaces;

namespace Projekt_Inżynierski.Service
{
    public class ImageService : IImageService
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageService(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public string SaveImage( IFormFile ImageFile )
        {
            string newFileName = _configuration["InsideRootPath"];
            newFileName += DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(ImageFile.FileName);

            using (var stream = System.IO.File.Create(_webHostEnvironment.WebRootPath + newFileName))
            {
                ImageFile.CopyTo(stream); 
            }

            return newFileName;
        }

        public void UpdateImage( IFormFile ImageFile, string oldFilePath )
        {
            System.IO.File.Delete(_webHostEnvironment.WebRootPath + oldFilePath);

            using (var stream = System.IO.File.Create(_webHostEnvironment.WebRootPath + oldFilePath))
            {
                ImageFile.CopyTo(stream);
            }

            return;
        }

        public void DeleteImage(string imagePath)
        {
            System.IO.File.Delete(_webHostEnvironment.WebRootPath + imagePath);
            return;
        }
    }
}