using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Interfaces
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile file, CancellationToken cancellationToken);
        void DeleteImage(string imageUrl);
    }
}
