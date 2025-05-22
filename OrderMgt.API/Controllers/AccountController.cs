using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderMgt.API.Interfaces;
using OrderMgt.API.Repositories;
using OrderMgt.Model.Entities;
using OrderMgt.Model.Models;
using Serialize.Linq.Serializers;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq.Expressions;

namespace OrderMgt.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        protected readonly ILogger<AccountController> _logger;
        protected readonly IAccountRepository repository;

        public AccountController(IAccountRepository _accountRepository, ILogger<AccountController> logger)
        {
            _logger = logger;
            repository = _accountRepository;
        }

        /// <summary>
        /// Adds a new account asynchronously.
        /// </summary>
        /// <param name="account">The account to be added. Cannot be null.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see cref="OkObjectResult"/>
        /// with the added account if the operation succeeds,  or <see cref="NotFoundObjectResult"/> if the operation
        /// fails.</returns>
        [HttpPost("AddAsync")]
        public async Task<IActionResult> AddAsync(Account account)
        {
            if (await repository.AddAsync(account))
            {
                return Ok(new BaseResponseModel { Success = true, Data = account });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false, ErrorMessage = "" });
            }
        }

        
        [HttpPost("AddRangeAsync")]
        public async Task<IActionResult> AddRangeAsync(IEnumerable<Account> accounts)
        {
            if (await repository.AddRangeAsync(accounts)) 
            {
                return Ok(new BaseResponseModel { Success = true, Data = accounts });
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

            Expression<Func<Account, bool>> result = (Expression<Func<Account, bool>>)serializer.DeserializeText(serializedExpression);

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
        public async Task<IActionResult> RemoveAsync(Account account)
        {
            if (await repository.RemoveAsync(account))
            {
                return Ok(new BaseResponseModel { Success = true });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }

        }

        [HttpDelete("RemoveRangeAsync")]
        public async Task<IActionResult> RemoveRangeAsync(IEnumerable<Account> accounts)
        {
            if (await repository.RemoveRangeAsync(accounts))
            {
                return Ok(new BaseResponseModel { Success = true });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }

        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(Account account)
        {
            if (await repository.UpdateAsync(account))
            {
                return Ok(new BaseResponseModel { Success = true, Data = account });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }

        [HttpPut("UpdateRangeAsync")]
        public async Task<IActionResult> UpdateRangeAsync(IEnumerable<Account> accounts)
        {
            if (await repository.UpdateRangeAsync(accounts))
            {
                return Ok(new BaseResponseModel { Success = true, Data = accounts });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }
    }
}
