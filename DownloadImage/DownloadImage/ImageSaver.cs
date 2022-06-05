using System;
using System.Collections.Generic;
using System.IO;

namespace DownloadImage
{
    public class ImageSaver
    {
        private readonly string _folderPath;

        public ImageSaver(string folderPath)
        {
            _folderPath = folderPath;
        }

        public void Save(IEnumerable<Image> images)
        {
            foreach (var image in images)
            {
                Save(image);
            }
        }

        public void Save(Image image)
        {
            if (!Directory.Exists(_folderPath)) Directory.CreateDirectory(_folderPath);

            var fileName = Path.GetFileNameWithoutExtension(image.Url.Pathname);
            var extension = Path.GetExtension(image.Url.Pathname);

            if (string.IsNullOrEmpty(extension))
            {
                extension = image.MimeType.Type switch
                {
                    "image/jpeg" => ".jpeg",
                    "image/png" => ".png",
                    "image/svg+xml" => ".svg",
                    _ => ".png"
                };
            }

            if (string.IsNullOrEmpty(fileName))
            {
                fileName = "image";
            }

            var i = 0;
            var fileNameWithExtension = fileName + extension;
            string savedImagePath;

            while (File.Exists(savedImagePath = Path.Combine(_folderPath, fileNameWithExtension)))
            {
                fileNameWithExtension = $"{fileName} ({i++}){extension}";
            }

            File.WriteAllBytes(savedImagePath, image.Content);
        }
    }
}