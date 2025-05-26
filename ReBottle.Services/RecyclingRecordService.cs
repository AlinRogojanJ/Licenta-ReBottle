using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using ReBottle.Models.DTOs;
using ReBottle.Models;
using ReBottle.Services.Interfaces;
using ReBottle.Services.Repositories;

namespace ReBottle.Services
{
    public class RecyclingRecordService : IRecyclingRecordService
    {
        private readonly IRecyclingRecordRepository _recyclingRecordRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IOrderStatusRepository _orderStatusRepository;
        private readonly IMapper _mapper;

        public RecyclingRecordService(IRecyclingRecordRepository recyclingRecordRepository, 
            IMapper mapper, 
            IUserRepository userRepository,
            ILocationRepository locationRepository,
            IOrderStatusRepository orderStatusRepository)
        {
            _recyclingRecordRepository = recyclingRecordRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _locationRepository = locationRepository;
            _orderStatusRepository = orderStatusRepository;
        }

        public async Task<IEnumerable<RecyclingRecordDTO>> GetAllRecyclingRecordsAsync()
        {
            var recyclingRecords = await _recyclingRecordRepository.GetAllRecyclingRecordsAsync();
            return _mapper.Map<IEnumerable<RecyclingRecordDTO>>(recyclingRecords);
        }

        public async Task<RecyclingRecordDTO> GetRecyclingRecordByIdAsync(Guid id)
        {
            var recyclingRecord = await _recyclingRecordRepository.GetRecyclingRecordByIdAsync(id);
            return _mapper.Map<RecyclingRecordDTO>(recyclingRecord);
        }

        // from:
        public async Task<Guid> AddRecyclingRecordAsync(Guid id, RecyclingRecordUpdateDTO request)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            var location = await _locationRepository.GetLocationByIdAsync(request.LocationId);
            var orderStatus = await _orderStatusRepository.GetOrderStatusByIdAsync(request.OrderStatusId);

            var recyclingRecord = new RecyclingRecord
            {
                RecyclingRecordId = Guid.NewGuid(),
                UserId = user!.UserId,
                LocationId = location!.LocationId,
                OrderStatusId = orderStatus.OrderStatusId,
                Amount = request.Amount,
                MoneySaved = (float)(request.Amount * 0.5),
                Method = request.Method,
                Date = request.Date,
                Created = DateTime.Now,
            };

            await _recyclingRecordRepository.AddRecyclingRecordAsync(recyclingRecord);

            return recyclingRecord.RecyclingRecordId;
        }


        public async Task DeleteRecyclingRecordAsync(Guid id)
        {
            await _recyclingRecordRepository.DeleteRecyclingRecordAsync(id);
        }

        public async Task<Dictionary<string, List<RecyclingReportDTO>>> GetUserRecordsGroupedByMonthAsync(Guid userId)
        {
            var records = await _recyclingRecordRepository.GetRecordsFromLastSixMonthsAsync(userId);

            var grouped = records
                .OrderByDescending(r => r.Date)
                .GroupBy(r => r.Date.ToString("MMM yyyy")) 
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(r => new RecyclingReportDTO
                    {
                        Date = r.Date.ToString("MMM dd, yyyy"),     
                        Amount = $"+{r.MoneySaved} RON",            
                        Progress = $"{r.Amount} bottles"          
                    }).ToList()
                );

            return grouped;
        }

        public async Task<List<MonthlyReportDTO>> GetMonthlyTotalsAsync(Guid userId)
        {
            var records = await _recyclingRecordRepository.GetRecordsFromLastSixMonthsAsync(userId);

            var groupedTotals = records
                .GroupBy(r => new { r.Date.Year, r.Date.Month })
                .OrderBy(g => g.Key.Year)
                .ThenBy(g => g.Key.Month)
                .Select(g => new MonthlyReportDTO
                {
                    Month = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMM"),
                    TotalBottles = g.Sum(r => r.Amount)
                })
                .ToList();

            return groupedTotals;
        }


    }
}
