using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReBottle.Models;

namespace ReBottle.Services.Interfaces
{
    public interface IImagesRepository
    {
        Task AddImageAsync(ImageStorage image);
        Task<ImageStorage?> GetImageByIdAsync(Guid id);
    }
}
