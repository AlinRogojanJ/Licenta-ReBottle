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
    public interface IOrderStatusService
    {
        Task<IEnumerable<OrderStatus>> GetAllOrderStatusAsync();
        Task AddOrderStatusAsync(OrderStatus OrderStatus);
        Task DeleteOrderStatusAsync(Guid id);
    }
}
