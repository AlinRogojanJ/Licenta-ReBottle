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

namespace ReBottle.Services
{
    public class OrderStatusService : IOrderStatusService
    {
        private readonly IOrderStatusRepository _orderStatusRepository;
        private readonly IMapper _mapper;

        public OrderStatusService(IOrderStatusRepository orderStatusRepository, IMapper mapper)
        {
            _orderStatusRepository = orderStatusRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderStatus>> GetAllOrderStatusAsync()
        {
            var orderStatuss = await _orderStatusRepository.GetAllOrderStatusAsync();
            return _mapper.Map<IEnumerable<OrderStatus>>(orderStatuss);
        }

        public async Task AddOrderStatusAsync(OrderStatus request)
        {
            var orderStatus = new OrderStatus
            {
                OrderStatusId = Guid.NewGuid(),
                OrderStatusName = request.OrderStatusName,
            };

            await _orderStatusRepository.AddOrderStatusAsync(orderStatus);

        }

        public async Task DeleteOrderStatusAsync(Guid id)
        {
            await _orderStatusRepository.DeleteOrderStatusAsync(id);
        }

    }
}
