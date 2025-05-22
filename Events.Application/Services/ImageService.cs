using Events.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly string _uploadsFolder;

        public ImageService()
        {
            _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(_uploadsFolder))
                Directory.CreateDirectory(_uploadsFolder);
        }

        public async Task<string> SaveImageAsync(IFormFile file, CancellationToken cancellationToken)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(ext))
                throw new DataException($"An unacceptable file format: {ext}");

            var fileName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(_uploadsFolder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream, cancellationToken);

            return $"/uploads/{fileName}";
        }

        public void DeleteImage(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
                return;

            var fileName = Path.GetFileName(imageUrl);
            var filePath = Path.Combine(_uploadsFolder, fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}
