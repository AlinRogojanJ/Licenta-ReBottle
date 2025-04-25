using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ReBottle.Models.Data;
using ReBottle.Models.DTOs;
using ReBottle.Models;
using ReBottle.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ReBottle.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public LocationService(ILocationRepository locationRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LocationGetDTO>> GetAllLocationsAsync()
        {
            var locations = await _locationRepository.GetAllLocationsAsync();
            return _mapper.Map<IEnumerable<LocationGetDTO>>(locations);
        }

        public async Task<LocationGetDTO> GetLocationByIdAsync(Guid id)
        {
            var location = await _locationRepository.GetLocationByIdAsync(id);
            return _mapper.Map<LocationGetDTO>(location);
        }

        public async Task AddLocationAsync(LocationDTO request)
        {
            var location = new Location
            {
                LocationId = Guid.NewGuid(),
                LocationName = request.LocationName,
                Address = request.Address,
                Longitude = request.Longitude,
                Latitude = request.Latitude,
                Image = request.Image,
                Schedule = request.Schedule,
                Created = DateTime.Now,
                Updated = DateTime.Now,
            };

            await _locationRepository.AddLocationAsync(location);

        }

        public async Task DeleteLocationAsync(Guid id)
        {
            await _locationRepository.DeleteLocationAsync(id);
        }

        public async Task<Location> UpdateLocationAsync(Guid locationId,
            JsonPatchDocument<LocationDTO> patchDoc)
        {
            var location = await _locationRepository.GetLocationByIdAsync(locationId);
            if (location == null)
            {
                throw new Exception($"Location not found with id: {locationId}");
            }

            var locationDto = _mapper.Map<LocationDTO>(location);

            patchDoc.ApplyTo(locationDto);
            _mapper.Map(locationDto, location);

            location.Updated = DateTime.Now;

            await _locationRepository.UpdateLocationAsync(location);
            return location;
        }
    }
}
