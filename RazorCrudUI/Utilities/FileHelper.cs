using Domain.Models;

namespace UI.Utilities
{
    public static class FileHelper
    {
        public static string UploadNewImage(IWebHostEnvironment environment, IFormFile file)
        {
            string guid = Guid.NewGuid().ToString();
            string ext = Path.GetExtension(file.FileName);

            string shortPath = Path.Combine("images\\Items", guid + ext);

            string path = Path.Combine(environment.WebRootPath, shortPath);

            using (var fs = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fs);
            }
            return shortPath;
        }

        public static void DeleteOldImage(IWebHostEnvironment environment,
     ItemModel item)
        {
            if (string.IsNullOrEmpty(item.PictureURL))
                return;
            string path = Path.Combine(environment.WebRootPath, item.PictureURL);
            if (!System.IO.File.Exists(path))
                return;
            System.IO.File.Delete(path);
        }
    }
}
