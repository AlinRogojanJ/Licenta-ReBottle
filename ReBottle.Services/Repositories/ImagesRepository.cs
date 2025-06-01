using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReBottle.Models;
using ReBottle.Models.Data;
using ReBottle.Services.Interfaces;

namespace ReBottle.Services.Repositories
{
    public class ImagesRepository : IImagesRepository
    {
        private readonly ReBottleContext _context;

        public ImagesRepository(ReBottleContext context)
        {
            _context = context;
        }

        public async Task AddImageAsync(ImageStorage image)
        {
            await _context.ImagesStorage.AddAsync(image);
            await _context.SaveChangesAsync();
        }

        public async Task<ImageStorage?> GetImageByIdAsync(Guid id)
        {
            return await _context.ImagesStorage.FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
