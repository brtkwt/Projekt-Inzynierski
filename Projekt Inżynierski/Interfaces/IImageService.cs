using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Inżynierski.Interfaces
{
    public interface IImageService
    {
        string SaveImage( IFormFile ImageFile );
        void UpdateImage( IFormFile ImageFile, string oldFilePath);
        void DeleteImage(string imagePath);
    }
}