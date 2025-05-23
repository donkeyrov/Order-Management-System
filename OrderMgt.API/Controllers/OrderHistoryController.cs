﻿using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class OrderHistoryController : ControllerBase
    {
        protected readonly IOrderHistoryRepository repository;
        protected readonly ILogger<OrderHistoryController> logger;

        public OrderHistoryController(IOrderHistoryRepository _orderHistoryRepository,ILogger<OrderHistoryController> _logger)
        {
            repository = _orderHistoryRepository;
            logger = _logger;
        }

        [HttpPost("AddAsync")]
        public async Task<IActionResult> AddAsync(OrderHistory orderHistory)
        {
            if (await repository.AddAsync(orderHistory))
            {
                return Ok(new BaseResponseModel { Success = true, Data = orderHistory });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false, ErrorMessage = "" });
            }
        }

        [HttpPost("AddRangeAsync")]
        public async Task<IActionResult> AddRangeAsync(IEnumerable<OrderHistory> orderHistories)
        {
            if (await repository.AddRangeAsync(orderHistories))
            {
                return Ok(new BaseResponseModel { Success = true, Data = orderHistories });
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

            Expression<Func<OrderHistory, bool>> result = (Expression<Func<OrderHistory, bool>>)serializer.DeserializeText(serializedExpression);

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
        public async Task<IActionResult> RemoveAsync(OrderHistory orderHistory)
        {
            if (await repository.RemoveAsync(orderHistory))
            {
                return Ok(new BaseResponseModel { Success = true });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }

        }

        [HttpDelete("RemoveRangeAsync")]
        public async Task<IActionResult> RemoveRangeAsync(IEnumerable<OrderHistory> orderHistories)
        {
            if (await repository.RemoveRangeAsync(orderHistories))
            {
                return Ok(new BaseResponseModel { Success = true });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }

        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(OrderHistory orderHistory)
        {
            if (await repository.UpdateAsync(orderHistory))
            {
                return Ok(new BaseResponseModel { Success = true, Data = orderHistory });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }

        [HttpPut("UpdateRangeAsync")]
        public async Task<IActionResult> UpdateRangeAsync(IEnumerable<OrderHistory> orderHistories)
        {
            if (await repository.UpdateRangeAsync(orderHistories))
            {
                return Ok(new BaseResponseModel { Success = true, Data = orderHistories });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }
    }
}
