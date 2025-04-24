using ReBottle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReBottle.Services.Interfaces
{
    public interface IOrderStatusRepository
    {
        Task<IEnumerable<OrderStatus>> GetAllOrderStatusAsync();
        Task AddOrderStatusAsync(OrderStatus OrderStatus);
        Task<OrderStatus> GetOrderStatusByIdAsync(Guid id);
        Task DeleteOrderStatusAsync(Guid id);
    }
}
