using Microsoft.EntityFrameworkCore;
using ReBottle.Models;
using ReBottle.Models.Data;
using ReBottle.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace ReBottle.Services.Repositories
{
    public class RecyclingRecordRepository(ReBottleContext reBottleContext, IMapper mapper) : IRecyclingRecordRepository
    {
        private readonly ReBottleContext _reBottleContext = reBottleContext;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<RecyclingRecord>> GetAllRecyclingRecordsAsync()
        {
            var records = await _reBottleContext.RecyclingRecords
                .Include(r => r.User)
                .ToListAsync();

            var result = _mapper.Map<List<RecyclingRecord>>(records);
            return result;
        }

        public async Task<RecyclingRecord?> GetRecyclingRecordByIdAsync(Guid id)
        {
            return await _reBottleContext.RecyclingRecords.FirstOrDefaultAsync(r => r.RecyclingRecordId == id);
        }

        public async Task AddRecyclingRecordAsync(RecyclingRecord recyclingRecord)
        {
            await _reBottleContext.RecyclingRecords.AddAsync(recyclingRecord);
            await _reBottleContext.SaveChangesAsync();
        }

        public async Task DeleteRecyclingRecordAsync(Guid id)
        {
            var recyclingRecord = await _reBottleContext.RecyclingRecords.FindAsync(id);

            if (recyclingRecord != null) _reBottleContext.RecyclingRecords.Remove(recyclingRecord);

            await _reBottleContext.SaveChangesAsync();
        }

        public async Task<List<RecyclingRecord>> GetRecordsFromLastSixMonthsAsync(Guid userId)
        {
            var sixMonthsAgo = DateTime.UtcNow.AddMonths(-6);

            return await _reBottleContext.RecyclingRecords
                .Where(r => r.UserId == userId && r.Date >= sixMonthsAgo)
                .ToListAsync();
        }
    }
}
