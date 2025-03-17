using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ReBottle.Models;
using ReBottle.Models.DTOs;
using ReBottle.Services.Interfaces;

namespace ReBottle.Web.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLocationsAsync()
        {
            var locations = await _locationService.GetAllLocationsAsync();
            if (locations == null) return NotFound();
            return Ok(locations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocationById(Guid id)
        {
            var location = await _locationService.GetLocationByIdAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        [HttpPost]
        public async Task<IActionResult> AddLocation([FromBody] LocationUpdateDTO locationUpdateDTO)
        {
            var location = new Location
            {
                LocationId = Guid.NewGuid(),
                LocationName = locationUpdateDTO.LocationName,
                Address = locationUpdateDTO.Address,
                Status = locationUpdateDTO.Status,
                Created = DateTime.Now,
                Updated = DateTime.Now
            };

            await _locationService.AddLocationAsync(location);

            return Ok("Location created successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocationAsync([FromRoute] Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _locationService.DeleteLocationAsync(id);
            return Ok("Location deleted successfully");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchLocation(Guid id,
            [FromBody] JsonPatchDocument<LocationUpdateDTO> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Patch document cannot be null.");
            }

            try
            {
                var updatedLocation = await _locationService.UpdateLocationAsync(id, patchDoc);
                return Ok(updatedLocation);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
