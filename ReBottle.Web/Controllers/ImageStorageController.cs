using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore;
using ReBottle.Models;
using ReBottle.Models.Data;
using ReBottle.Web.Requests;
using Microsoft.AspNetCore.Authorization;

namespace ReBottle.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ImageStorageController : ControllerBase
    {
        private readonly ReBottleContext _db;
        public ImageStorageController(ReBottleContext db) => _db = db;

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload([FromForm] UploadImageRequest request)
        {
            if (request.File.Length == 0)
                return BadRequest("No file submitted.");

           
            var user = await _db.Users.FindAsync(request.UserId);
            if (user == null)
                return NotFound($"User {request.UserId} not found.");

            await using var ms = new MemoryStream();
            await request.File.CopyToAsync(ms);

            var img = new ImageStorage()
            {
                FileName = request.File.FileName,
                ContentType = request.File.ContentType!,
                Data = ms.ToArray(),
                UserId = request.UserId,
                RecyclingRecordId = request.RecyclingRecordId,
            };

            _db.ImagesStorage.Add(img);
            await _db.SaveChangesAsync();
            return Ok(new { img.Id });
        }
        // GET api/imageupload/{id}
        [HttpGet("{id}/imagine")]
        public async Task<IActionResult> Get(Guid id)
        {
            var img = await _db.ImagesStorage.FindAsync(id);
            if (img == null) return NotFound();

            return File(img.Data, img.ContentType, img.FileName);
        }

        // (Optional) GET all images for a user
        // GET api/imageupload/user/{userId}
        [HttpGet("user/{userId}/image")]
        public async Task<IActionResult> GetByUser(Guid userId)
        {
            var images = await _db.ImagesStorage
                                  .AsNoTracking()
                                  .Where(i => i.UserId == userId)
                                  .Select(i => new { i.Id, i.FileName })
                                  .ToListAsync();

            return Ok(images);
        }
    }
}
