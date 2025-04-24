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
        Task<IEnumerable<OrderStatusGetDTO>> GetAllOrderStatusAsync();
        Task AddOrderStatusAsync(OrderStatusDTO OrderStatus);
        Task<OrderStatusGetDTO> GetOrderStatusByIdAsync(Guid id);
        Task DeleteOrderStatusAsync(Guid id);
    }
}
