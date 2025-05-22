using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderMgt.API.Interfaces;
using OrderMgt.Model.Entities;
using OrderMgt.Model.Models;
using Serialize.Linq.Serializers;
using System.Linq.Expressions;

namespace OrderMgt.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        protected readonly IPromotionRepository repository;
        protected readonly ILogger<PromotionController> logger;

        public PromotionController(IPromotionRepository promotionRepository,ILogger<PromotionController> _logger)
        {
            repository = promotionRepository;   
            logger = _logger;
        }

        [HttpPost("AddAsync")]
        public async Task<IActionResult> AddAsync(Promotion promotion)
        {
            if (await repository.AddAsync(promotion))
            {
                return Ok(new BaseResponseModel { Success = true, Data = promotion });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false, ErrorMessage = "" });
            }
        }

        [HttpPost("AddRangeAsync")]
        public async Task<IActionResult> AddRangeAsync(IEnumerable<Promotion> promotions)
        {
            if (await repository.AddRangeAsync(promotions))
            {
                return Ok(new BaseResponseModel { Success = true, Data = promotions });
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

            Expression<Func<Promotion, bool>> result = (Expression<Func<Promotion, bool>>)serializer.DeserializeText(serializedExpression);

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
        public async Task<IActionResult> RemoveAsync(Promotion promotion)
        {
            if (await repository.RemoveAsync(promotion))
            {
                return Ok(new BaseResponseModel { Success = true });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }

        }

        [HttpDelete("RemoveRangeAsync")]
        public async Task<IActionResult> RemoveRangeAsync(IEnumerable<Promotion> promotions)
        {
            if (await repository.RemoveRangeAsync(promotions))
            {
                return Ok(new BaseResponseModel { Success = true });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }

        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(Promotion promotion)
        {
            if (await repository.UpdateAsync(promotion))
            {
                return Ok(new BaseResponseModel { Success = true, Data = promotion });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }

        [HttpPut("UpdateRangeAsync")]
        public async Task<IActionResult> UpdateRangeAsync(IEnumerable<Promotion> promotions)
        {
            if (await repository.UpdateRangeAsync(promotions))
            {
                return Ok(new BaseResponseModel { Success = true, Data = promotions });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }
    }
}
