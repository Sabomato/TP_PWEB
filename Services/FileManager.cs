using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TP_PWEB.Data;
using TP_PWEB.Models;

namespace TP_PWEB.Services
{
    public static class FileManager
    {


        public static async Task<ICollection<Image>> ConvertImagesAsync(List<IFormFile> files)
        {
            ICollection<Image> Images = new List<Image>();
            foreach (var file in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var contentType = file.ContentType;
                var extension = Path.GetExtension(file.FileName);

                if (!extension.Equals(".jpg") && !extension.Equals(".png"))
                    continue;

                var image = new Image
                {
                    ContentType = contentType,
                    Name = fileName
                };

                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    image.Content = dataStream.ToArray();
                }
                Images.Add(image);
                
            }
            
            return Images;
        }
    }
}
