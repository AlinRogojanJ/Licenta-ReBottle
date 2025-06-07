using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReBottle.Models.DTOs;
using ReBottle.Models;
using ReBottle.Services.Interfaces;
using ReBottle.Services;

namespace ReBottle.Web.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RecyclingRecordController : ControllerBase
    {
        private readonly IRecyclingRecordService _recyclingRecordService;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public RecyclingRecordController(IRecyclingRecordService recyclingRecordService, IImageService imageService)
        {
            _recyclingRecordService = recyclingRecordService;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLocationsAsync()
        {
            var locations = await _recyclingRecordService.GetAllRecyclingRecordsAsync();
            if (locations == null) return NotFound();
            return Ok(locations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocationById(Guid id)
        {
            var location = await _recyclingRecordService.GetRecyclingRecordByIdAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> AddRecycle(
            [FromRoute] Guid id,
            [FromBody] RecyclingRecordUpdateDTO request)
        {
            var newRecordId = await _recyclingRecordService
                .AddRecyclingRecordAsync(id, request);

            return Ok(new { recyclingRecordId = newRecordId });

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecyclingRecord([FromRoute] Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _recyclingRecordService.DeleteRecyclingRecordAsync(id);

            return Ok("Recyling Record deleted successfully");
        }

        [HttpGet("grouped-by-month/{userId}")]
        public async Task<IActionResult> GetUserRecordsGroupedByMonth(Guid userId)
        {
            var groupedRecords = await _recyclingRecordService.GetUserRecordsGroupedByMonthAsync(userId);
            return Ok(groupedRecords);
        }

        [HttpGet("monthly-totals/{userId}")]
        public async Task<IActionResult> GetMonthlyTotals(Guid userId)
        {
            var monthlyTotals = await _recyclingRecordService.GetMonthlyTotalsAsync(userId);
            return Ok(monthlyTotals);
        }


    }
}
