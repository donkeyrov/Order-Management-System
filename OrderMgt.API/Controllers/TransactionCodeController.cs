using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderMgt.API.Repositories;
using OrderMgt.Model.Entities;
using OrderMgt.Model.Models;
using Serialize.Linq.Serializers;
using System.Linq.Expressions;

namespace OrderMgt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionCodeController : ControllerBase
    {
        protected readonly TransactionCodeRepository repository;
        protected readonly ILogger<TransactionCodeController> logger;

        public TransactionCodeController(TransactionCodeRepository _transactionCodeRepository, ILogger<TransactionCodeController> _logger)
        {
            repository = _transactionCodeRepository;
            logger = _logger;
        }

        [HttpPost("AddAsync")]
        public async Task<IActionResult> AddAsync(TransactionCode transactionCode)
        {
            if (await repository.AddAsync(transactionCode))
            {
                return Ok(new BaseResponseModel { Success = true, Data = transactionCode });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false, ErrorMessage = "" });
            }
        }

        [HttpPost("AddRangeAsync")]
        public async Task<IActionResult> AddRangeAsync(IEnumerable<TransactionCode> transactionCodes)
        {
            if (await repository.AddRangeAsync(transactionCodes))
            {
                return Ok(new BaseResponseModel { Success = true, Data = transactionCodes });
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

            Expression<Func<TransactionCode, bool>> result = (Expression<Func<TransactionCode, bool>>)serializer.DeserializeText(serializedExpression);

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
        public async Task<IActionResult> RemoveAsync(TransactionCode transactionCode)
        {
            if (await repository.RemoveAsync(transactionCode))
            {
                return Ok(new BaseResponseModel { Success = true });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }

        }

        [HttpDelete("RemoveRangeAsync")]
        public async Task<IActionResult> RemoveRangeAsync(IEnumerable<TransactionCode> transactionCodes)
        {
            if (await repository.RemoveRangeAsync(transactionCodes))
            {
                return Ok(new BaseResponseModel { Success = true });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }

        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(TransactionCode transactionCode)
        {
            if (await repository.UpdateAsync(transactionCode))
            {
                return Ok(new BaseResponseModel { Success = true, Data = transactionCode });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }

        [HttpPut("UpdateRangeAsync")]
        public async Task<IActionResult> UpdateRangeAsync(IEnumerable<TransactionCode> transactionCodes)
        {
            if (await repository.UpdateRangeAsync(transactionCodes))
            {
                return Ok(new BaseResponseModel { Success = true, Data = transactionCodes });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }
    }
}
