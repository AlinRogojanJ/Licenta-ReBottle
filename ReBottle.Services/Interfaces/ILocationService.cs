using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using ReBottle.Models;
using ReBottle.Models.DTOs;

namespace ReBottle.Services.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationDTO>> GetAllLocationsAsync();
        Task<LocationDTO> GetLocationByIdAsync(Guid id);
        Task AddLocationAsync(Location location);
        Task<Location> UpdateLocationAsync(Guid id, JsonPatchDocument<LocationUpdateDTO> patchDoc);
        Task DeleteLocationAsync(Guid id);
    }
}
