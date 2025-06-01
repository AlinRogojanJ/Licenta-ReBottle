using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReBottle.Models;
using ReBottle.Models.DTOs;

namespace ReBottle.Services.Interfaces
{
    public interface IImageService
    {
        Task<ImageStorage> SaveImageAsync(IFormFile image);
        Task<ImageStorage> GetImageByIdAsync(Guid id);
    }
}
