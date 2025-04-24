using ReBottle.Models.Data;
using ReBottle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReBottle.Services.Interfaces;

namespace ReBottle.Services.Repositories
{
    public class OrderStatusRepository(ReBottleContext reBottleContext) : IOrderStatusRepository
    {
        private readonly ReBottleContext _reBottleContext = reBottleContext;

        public async Task<IEnumerable<OrderStatus>> GetAllOrderStatusAsync()
        {
            return await _reBottleContext.OrderStatuses.ToListAsync();
        }

        public async Task AddOrderStatusAsync(OrderStatus orderStatus)
        {
            await _reBottleContext.OrderStatuses.AddAsync(orderStatus);
            await _reBottleContext.SaveChangesAsync();
        }

        public async Task<OrderStatus> GetOrderStatusByIdAsync(Guid id)
        {
            return await _reBottleContext.OrderStatuses.FirstOrDefaultAsync(r => r.OrderStatusId == id);
        }

        public async Task DeleteOrderStatusAsync(Guid id)
        {
            var orderStatus = await _reBottleContext.OrderStatuses.FindAsync(id);

            if (orderStatus != null) _reBottleContext.OrderStatuses.Remove(orderStatus);

            await _reBottleContext.SaveChangesAsync();
        }
    }
}
