using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ReBottle.Models.DTOs;
using ReBottle.Models;
using ReBottle.Services.Interfaces;
using Azure.Core;

namespace ReBottle.Web.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderStatusController : ControllerBase
    {
        private readonly IOrderStatusService _orderStatusService;
        private readonly IMapper _mapper;

        public OrderStatusController(IOrderStatusService orderStatusService)
        {
            _orderStatusService = orderStatusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderStatusAsync()
        {
            var orderStatus = await _orderStatusService.GetAllOrderStatusAsync();
            if (orderStatus == null) return NotFound();
            return Ok(orderStatus);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrderStatus([FromBody] OrderStatus request)
        {
            var orderStatus = new OrderStatus
            {
                OrderStatusId = Guid.NewGuid(),
                OrderStatusName = request.OrderStatusName,
            };

            await _orderStatusService.AddOrderStatusAsync(orderStatus);

            return Ok("OrderStatus created successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderStatusAsync([FromRoute] Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _orderStatusService.DeleteOrderStatusAsync(id);
            return Ok("OrderStatus deleted successfully");
        }
    }
}
