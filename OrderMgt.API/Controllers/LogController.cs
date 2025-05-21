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
    public class LogController : ControllerBase
    {
        protected readonly ILogRepository repository;
        protected readonly ILogger<LogController> logger;
        public LogController(LogRepository _logRepository,ILogger<LogController> _logger)
        {
            repository = _logRepository;
            logger = _logger;
        }

        [HttpPost("AddAsync")]
        public async Task<IActionResult> AddAsync(Log log)
        {
            if (await repository.AddAsync(log))
            {
                return Ok(new BaseResponseModel { Success = true, Data = log });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false, ErrorMessage = "" });
            }
        }

        [HttpPost("AddRangeAsync")]
        public async Task<IActionResult> AddRangeAsync(IEnumerable<Log> logs)
        {
            if (await repository.AddRangeAsync(logs))
            {
                return Ok(new BaseResponseModel { Success = true, Data = logs });
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

            Expression<Func<Log, bool>> result = (Expression<Func<Log, bool>>)serializer.DeserializeText(serializedExpression);

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
        public async Task<IActionResult> RemoveAsync(Log log)
        {
            if (await repository.RemoveAsync(log))
            {
                return Ok(new BaseResponseModel { Success = true });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }

        }

        [HttpDelete("RemoveRangeAsync")]
        public async Task<IActionResult> RemoveRangeAsync(IEnumerable<Log> logs)
        {
            if (await repository.RemoveRangeAsync(logs))
            {
                return Ok(new BaseResponseModel { Success = true });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }

        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(Log log)
        {
            if (await repository.UpdateAsync(log))
            {
                return Ok(new BaseResponseModel { Success = true, Data = log });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }

        [HttpPut("UpdateRangeAsync")]
        public async Task<IActionResult> UpdateRangeAsync(IEnumerable<Log> logs)
        {
            if (await repository.UpdateRangeAsync(logs))
            {
                return Ok(new BaseResponseModel { Success = true, Data = logs });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }
    }
}
