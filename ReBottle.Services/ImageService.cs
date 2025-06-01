using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ReBottle.Models;
using ReBottle.Models.DTOs;
using ReBottle.Services.Interfaces;
using ReBottle.Services.Repositories;

namespace ReBottle.Services
{
    public class ImageService : IImageService
    {
        private readonly IImagesRepository _imageRepo;
        private readonly IMapper _mapper;

        public ImageService(IImagesRepository imageRepo)
        {
            _imageRepo = imageRepo;
        }

        public async Task<ImageStorage> SaveImageAsync(IFormFile image)
        {
            using var memoryStream = new MemoryStream();
            await image.CopyToAsync(memoryStream);

            var imageEntity = new ImageStorage
            {
                Id = Guid.NewGuid(),
                FileName = image.FileName,
                ContentType = image.ContentType,
                Data = memoryStream.ToArray()
            };

            await _imageRepo.AddImageAsync(imageEntity);

            return imageEntity;
        }

        public async Task<ImageStorage> GetImageByIdAsync(Guid id)
        {
            var orderStatus = await _imageRepo.GetImageByIdAsync(id);
            return _mapper.Map<ImageStorage>(orderStatus);
        }

    }
}
