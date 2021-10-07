using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Areas.Organizer.Helpers
{
    public class Image
    {
        public static string Upload(IFormFile image, IHostingEnvironment webhost, string modelName)
        {

            string uniqueFileName = null;

            if (image != null)
            {
                string uploadsFolder = Path.Combine(webhost.WebRootPath, "images", modelName);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public static void Delete(IHostingEnvironment webhost, string folder, string fileName)
        {
            string fullPath = Path.Combine(webhost.WebRootPath, "images", folder, fileName);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}
