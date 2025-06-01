using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore;
using ReBottle.Models;
using ReBottle.Models.Data;
using ReBottle.Web.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Newtonsoft.Json;
using ReBottle.Models.DTOs;
using System.Globalization;
using ReBottle.Services.Interfaces;

namespace ReBottle.Web.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ImageStorageController : ControllerBase
    {
        private readonly ReBottleContext _db;
        private readonly IRecyclingRecordService _recyclingRecordService;

        public ImageStorageController(ReBottleContext db, IRecyclingRecordService recyclingRecordService)
        {
            _db = db;
            _recyclingRecordService = recyclingRecordService;
        }

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
                Id = Guid.NewGuid(),
                FileName = request.File.FileName,
                ContentType = request.File.ContentType!,
                Data = ms.ToArray(),
                UserId = request.UserId,
            };

            _db.ImagesStorage.Add(img);
            await _db.SaveChangesAsync();
            return Ok(new { img.Id });
        }

        [HttpPost("scan")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ScanUpload([FromForm] UploadImageRequest request)
        {
            if (request.File.Length == 0)
                return BadRequest("No file submitted.");

            var user = await _db.Users.FindAsync(request.UserId);
            if (user == null)
                return NotFound($"User {request.UserId} not found.");

            await using var ms = new MemoryStream();
            await request.File.CopyToAsync(ms);
            var imageBytes = ms.ToArray();

            var img = new ImageStorage
            {
                Id = Guid.NewGuid(),
                FileName = request.File.FileName,
                ContentType = request.File.ContentType!,
                Data = imageBytes,
                UserId = request.UserId
            };

            _db.ImagesStorage.Add(img);
            await _db.SaveChangesAsync();

            // Send image to OCR Python API
            using var httpClient = new HttpClient();
            using var content = new MultipartFormDataContent();
            var imageContent = new ByteArrayContent(imageBytes);
            imageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(request.File.ContentType);
            content.Add(imageContent, "file", request.File.FileName);

            HttpResponseMessage ocrResponse;
            try
            {
                ocrResponse = await httpClient.PostAsync("http://localhost:8000/predict", content);
                ocrResponse.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"OCR server error: {ex.Message}");
            }

            var ocrResult = await ocrResponse.Content.ReadAsStringAsync();

            var ocrObject = JsonConvert.DeserializeObject<OcrResult>(ocrResult);

            DateTime.TryParseExact(
                ocrObject.date,
                "dd-MMM-yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime parsedDate);

            var locationId = Guid.Parse("E9AA66E2-2179-4AAF-A558-0C39BE1581F3");
            var orderStatusId = Guid.Parse("C2E40075-6013-4F0D-81FA-A449C0426462");
            var method = "scan";

            var recycleDto = new RecyclingRecordUpdateDTO
            {
                UserId = request.UserId,
                LocationId = locationId,
                OrderStatusId = orderStatusId,
                ImageId = img.Id,
                MoneySaved = float.Parse(ocrObject.total_price, CultureInfo.InvariantCulture),
                Method = method,
                Date = parsedDate
            };



            var newRecordId = await _recyclingRecordService.AddRecyclingRecordAsync(request.UserId, recycleDto);

            return Ok(new
            {
                imageId = img.Id,
                ocr = ocrObject,
                recyclingRecordId = newRecordId
            });
        }


        [HttpGet("{id}/imagine")]
        public async Task<IActionResult> Get(Guid id)
        {
            var img = await _db.ImagesStorage.FindAsync(id);
            if (img == null) return NotFound();

            return File(img.Data, img.ContentType, img.FileName);
        }

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
