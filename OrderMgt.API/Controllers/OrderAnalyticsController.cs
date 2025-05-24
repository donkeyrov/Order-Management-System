using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderMgt.API.Interfaces;
using OrderMgt.API.Services;
using OrderMgt.Model.Models;

namespace OrderMgt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderAnalyticsController : ControllerBase
    {
        protected readonly IOrderRepository orderRepository;
        protected readonly IOrderHistoryRepository orderHistoryRepository;
        protected readonly IOrderService orderService;
        protected readonly ILogger<OrderAnalyticsController> logger;

        public OrderAnalyticsController(IOrderRepository orderRepo, IOrderService _service ,IOrderHistoryRepository orderHistRepo,ILogger<OrderAnalyticsController> _logger)
        {
            orderRepository = orderRepo;
            orderHistoryRepository = orderHistRepo;
            orderService = _service;
            logger = _logger;
        }

        [HttpGet("OrderStatisticsAsync")]
        public async Task<IActionResult> OrderStatisticsAsync(int id)
        {
            var result = await orderService.GetOrderStats();
            if (result.Success)
            {
                return Ok(new BaseResponseModel() { Success = true,Data = result.Data });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false, ErrorMessage = "Could not complete the order!" });
            }
        }
    }
}
