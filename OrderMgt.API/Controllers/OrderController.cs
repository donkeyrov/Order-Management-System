using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderMgt.API.Interfaces;
using OrderMgt.API.Repositories;
using OrderMgt.Model.Entities;
using OrderMgt.Model.Models;
using Serialize.Linq.Serializers;
using System.Linq.Expressions;

namespace OrderMgt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        protected readonly IOrderRepository orderRepository;
        protected readonly IOrderHistoryRepository orderHistoryRepository;       
        protected readonly IOrderService orderService;
        protected readonly ILogger<OrderController> logger;
        public OrderController(OrderRepository _orderRepository,IOrderHistoryRepository _orderHistoryRepository,
            IOrderService _orderService , ILogger<OrderController> _logger)
        {
            orderRepository = _orderRepository;
            orderHistoryRepository = _orderHistoryRepository;            
            orderService = _orderService;
            logger = _logger;
        }

        [HttpPost("AddAsync")]
        public async Task<IActionResult> AddAsync(Order order)
        {
            if (await orderRepository.AddAsync(order))
            {
                return Ok(new BaseResponseModel { Success = true, Data = order });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false, ErrorMessage = "" });
            }
        }

        [HttpPost("AddRangeAsync")]
        public async Task<IActionResult> AddRangeAsync(IEnumerable<Order> orders)
        {
            if (await orderRepository.AddRangeAsync(orders))
            {
                return Ok(new BaseResponseModel { Success = true, Data = orders });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false, ErrorMessage = "" });
            }
        }

        [HttpGet("Find")]
        public IActionResult Find(string predicate)
        {
            var serializer = new ExpressionSerializer(new Serialize.Linq.Serializers.JsonSerializer());
            string serializedExpression = predicate.Replace('@', '#');

            Expression<Func<Order, bool>> result = (Expression<Func<Order, bool>>)serializer.DeserializeText(serializedExpression);

            var res = orderRepository.Find(result);
            if (res.Count() > 0)
            {
                return Ok(new BaseResponseModel { Success = true, Data = res });
            }
            else
            {
                return Ok(new BaseResponseModel { Success = false, Data = null });
            }
        }

        [HttpGet("ProcessOrderAsync")]
        public async Task<IActionResult> ProcessOrderAsync(int id)
        {
            var result = await orderService.ProcessOrder(id);

            if (result.Success)
            {
                return Ok(new BaseResponseModel() { Success = true });
            }
            else 
            {
                return NotFound(new BaseResponseModel { Success = false,ErrorMessage="Could not process the order!" });
            }
        }

        [HttpGet("CompleteOrderAsync")]
        public async Task<IActionResult> CompleteOrderAsync(int id)
        {
            var result = await orderService.CompleteOrder(id, User.Identity.Name);
            if (result.Success)
            {
                return Ok(new BaseResponseModel() { Success = true });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false, ErrorMessage = "Could not complete the order!" });
            }
        }

        [HttpGet("GetAsync")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var res = await orderRepository.GetAsync(id);
            return Ok(new BaseResponseModel { Success = true, Data = res });
        }

        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var res = await orderRepository.GetAllAsync();
            return Ok(new BaseResponseModel { Success = true, Data = res });
        }

        [HttpDelete("RemoveAsync")]
        public async Task<IActionResult> RemoveAsync(Order order)
        {
            if (await orderRepository.RemoveAsync(order))
            {
                return Ok(new BaseResponseModel { Success = true });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }

        }

        [HttpDelete("RemoveRangeAsync")]
        public async Task<IActionResult> RemoveRangeAsync(IEnumerable<Order> orders)
        {
            if (await orderRepository.RemoveRangeAsync(orders))
            {
                return Ok(new BaseResponseModel { Success = true });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }

        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(Order order)
        {
            if (await orderRepository.UpdateAsync(order))
            {
                return Ok(new BaseResponseModel { Success = true, Data = order });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }

        [HttpPut("UpdateRangeAsync")]
        public async Task<IActionResult> UpdateRangeAsync(IEnumerable<Order> orders)
        {
            if (await orderRepository.UpdateRangeAsync(orders))
            {
                return Ok(new BaseResponseModel { Success = true, Data = orders });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }
    }
}
