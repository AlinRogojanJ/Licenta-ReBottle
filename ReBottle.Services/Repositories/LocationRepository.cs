using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReBottle.Models.Data;
using ReBottle.Models;
using ReBottle.Services.Interfaces;

namespace ReBottle.Services.Repositories
{
    public class LocationRepository(ReBottleContext reBottleContext) : ILocationRepository
    {
        private readonly ReBottleContext _reBottleContext = reBottleContext;

        public async Task<IEnumerable<Location>> GetAllLocationsAsync()
        {
            return await _reBottleContext.Locations.ToListAsync();
        }

        public async Task<Location?> GetLocationByIdAsync(Guid id)
        {
            return await _reBottleContext.Locations.FirstOrDefaultAsync(r => r.LocationId == id);
        }

        public async Task AddLocationAsync(Location location)
        {
            await _reBottleContext.Locations.AddAsync(location);
            await _reBottleContext.SaveChangesAsync();
        }

        public async Task DeleteLocationAsync(Guid id)
        {
            var location = await _reBottleContext.Locations.FindAsync(id);

            if (location != null) _reBottleContext.Locations.Remove(location);

            await _reBottleContext.SaveChangesAsync();
        }

        public async Task UpdateLocationAsync(Location location)
        {
            _reBottleContext.Locations.Update(location);
            await _reBottleContext.SaveChangesAsync();
        }
    }
}
