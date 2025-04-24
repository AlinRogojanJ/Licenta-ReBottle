using ReBottle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReBottle.Services.Interfaces
{
    public interface IRecyclingRecordRepository
    {
        Task<IEnumerable<RecyclingRecord>> GetAllRecyclingRecordsAsync();
        Task<RecyclingRecord?> GetRecyclingRecordByIdAsync(Guid id);
        Task AddRecyclingRecordAsync(RecyclingRecord recyclingRecord);
        Task DeleteRecyclingRecordAsync(Guid id);
    }
}
