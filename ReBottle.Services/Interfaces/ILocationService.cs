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
        Task<IEnumerable<LocationGetDTO>> GetAllLocationsAsync();
        Task<LocationGetDTO> GetLocationByIdAsync(Guid id);
        Task AddLocationAsync(LocationDTO location);
        Task<Location> UpdateLocationAsync(Guid id, JsonPatchDocument<LocationDTO> patchDoc);
        Task DeleteLocationAsync(Guid id);
    }
}
