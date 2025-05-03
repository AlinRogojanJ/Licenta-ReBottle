using Microsoft.AspNetCore.JsonPatch;
using ReBottle.Models.DTOs;
using ReBottle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReBottle.Services.Interfaces
{
    public interface IRecyclingRecordService
    {
        Task<IEnumerable<RecyclingRecordDTO>> GetAllRecyclingRecordsAsync();
        Task<RecyclingRecordDTO> GetRecyclingRecordByIdAsync(Guid id);
        Task AddRecyclingRecordAsync(RecyclingRecordUpdateDTO recyclingRecord);
        Task DeleteRecyclingRecordAsync(Guid id);
        Task<Dictionary<string, List<RecyclingReportDTO>>> GetUserRecordsGroupedByMonthAsync(Guid userId);
        Task<List<MonthlyReportDTO>> GetMonthlyTotalsAsync(Guid userId);

    }
}
