using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class OrderProductController : ControllerBase
    {
        protected readonly IOrderProductRepository repository;
        protected readonly ILogger<OrderProductController> logger;

        public OrderProductController(OrderProductRepository _orderProductRepository, ILogger<OrderProductController> _logger)
        {
            repository = _orderProductRepository;
            logger = _logger;
        }

        [HttpPost("AddAsync")]
        public async Task<IActionResult> AddAsync(OrderProduct orderProduct)
        {
            if (await repository.AddAsync(orderProduct))
            {
                return Ok(new BaseResponseModel { Success = true, Data = orderProduct });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false, ErrorMessage = "" });
            }
        }

        [HttpPost("AddRangeAsync")]
        public async Task<IActionResult> AddRangeAsync(IEnumerable<OrderProduct> orderProducts)
        {
            if (await repository.AddRangeAsync(orderProducts))
            {
                return Ok(new BaseResponseModel { Success = true, Data = orderProducts });
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

            Expression<Func<OrderProduct, bool>> result = (Expression<Func<OrderProduct, bool>>)serializer.DeserializeText(serializedExpression);

            var res = repository.Find(result);
            if (res.Count() > 0)
            {
                return Ok(new BaseResponseModel { Success = true, Data = res });
            }
            else
            {
                return Ok(new BaseResponseModel { Success = false, Data = null });
            }
        }

        [HttpGet("GetAsync")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var res = await repository.GetAsync(id);
            return Ok(new BaseResponseModel { Success = true, Data = res });
        }

        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var res = await repository.GetAllAsync();
            return Ok(new BaseResponseModel { Success = true, Data = res });
        }

        [HttpDelete("RemoveAsync")]
        public async Task<IActionResult> RemoveAsync(OrderProduct orderProduct)
        {
            if (await repository.RemoveAsync(orderProduct))
            {
                return Ok(new BaseResponseModel { Success = true });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }

        }

        [HttpDelete("RemoveRangeAsync")]
        public async Task<IActionResult> RemoveRangeAsync(IEnumerable<OrderProduct> orderProducts)
        {
            if (await repository.RemoveRangeAsync(orderProducts))
            {
                return Ok(new BaseResponseModel { Success = true });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }

        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(OrderProduct orderProduct)
        {
            if (await repository.UpdateAsync(orderProduct))
            {
                return Ok(new BaseResponseModel { Success = true, Data = orderProduct });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }

        [HttpPut("UpdateRangeAsync")]
        public async Task<IActionResult> UpdateRangeAsync(IEnumerable<OrderProduct> orderProducts)
        {
            if (await repository.UpdateRangeAsync(orderProducts))
            {
                return Ok(new BaseResponseModel { Success = true, Data = orderProducts });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }
    }
}
