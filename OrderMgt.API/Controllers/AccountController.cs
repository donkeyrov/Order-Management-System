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
    /// <summary>
    /// Provides API endpoints for managing accounts, including operations such as adding, updating, retrieving, and
    /// removing accounts.
    /// </summary>
    /// <remarks>This controller is responsible for handling account-related operations in the system. It
    /// provides methods to perform CRUD operations on accounts, both individually and in bulk. All endpoints require
    /// authorization and are accessible via the "api/Account" route.</remarks>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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

        /// <summary>
        /// Adds a collection of accounts to the repository asynchronously.
        /// </summary>
        /// <remarks>This method attempts to add all provided accounts to the repository.  Ensure that the
        /// <paramref name="accounts"/> parameter contains valid account objects.</remarks>
        /// <param name="accounts">The collection of <see cref="Account"/> objects to be added. Cannot be null or empty.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see cref="OkObjectResult"/>
        /// with a success response and the added accounts if the operation succeeds.  Returns <see
        /// cref="NotFoundObjectResult"/> with an error response if the operation fails.</returns>
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

        /// <summary>
        /// Finds and retrieves a collection of accounts that match the specified predicate.
        /// </summary>
        /// <remarks>The <paramref name="predicate"/> parameter must be a valid serialized LINQ expression
        /// that can be deserialized into an  <see cref="Expression{TDelegate}"/> of type
        /// <c>Expression&lt;Func&lt;Account, bool&gt;&gt;</c>.  Ensure the input is properly sanitized and formatted
        /// before calling this method.</remarks>
        /// <param name="predicate">A serialized string representation of a LINQ expression used to filter accounts.  The string must be
        /// properly formatted and replace '@' with '#' to ensure correct deserialization.</param>
        /// <returns>An <see cref="IActionResult"/> containing a <see cref="BaseResponseModel"/>.  If matching accounts are
        /// found, the response's <c>Success</c> property is <see langword="true"/> and <c>Data</c> contains the
        /// results.  Otherwise, <c>Success</c> is <see langword="false"/> and <c>Data</c> is <see langword="null"/>.</returns>
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

        /// <summary>
        /// Retrieves an account by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the item to retrieve. Must be a positive integer.</param>
        /// <returns>An <see cref="IActionResult"/> containing a <see cref="BaseResponseModel"/> with the operation's success
        /// status  and the retrieved item as the data. Returns a 200 OK response if the operation is successful.</returns>
        [HttpGet("GetAsync")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var res = await repository.GetAsync(id);
            return Ok(new BaseResponseModel { Success = true, Data = res });
        }

        /// <summary>
        /// Retrieves all accounts asynchronously.
        /// </summary>
        /// <remarks>This method fetches all items from the underlying data source and returns them in the
        /// response.         The response includes a success flag and the retrieved data.</remarks>
        /// <returns>An <see cref="IActionResult"/> containing a <see cref="BaseResponseModel"/> with a success flag set to <see
        /// langword="true"/>          and the data retrieved from the repository.</returns>
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var res = await repository.GetAllAsync();
            return Ok(new BaseResponseModel { Success = true, Data = res });
        }

        /// <summary>
        /// Removes the specified account from the system.
        /// </summary>
        /// <param name="account">The account to be removed. Must not be <see langword="null"/>.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see cref="OkObjectResult"/>
        /// with a success response if the account was removed successfully;  otherwise, returns <see
        /// cref="NotFoundObjectResult"/> with a failure response if the account could not be found.</returns>
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

        /// <summary>
        /// Removes a range of accounts from the repository.
        /// </summary>
        /// <remarks>This method attempts to remove the specified accounts from the repository.  If the
        /// operation is successful, a success response is returned.  If the accounts cannot be found or removed, a
        /// failure response is returned.</remarks>
        /// <param name="accounts">The collection of accounts to be removed. Cannot be null or empty.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see cref="OkObjectResult"/>
        /// with a success response if the accounts were removed successfully;  otherwise, returns <see
        /// cref="NotFoundObjectResult"/> with a failure response.</returns>
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

        /// <summary>
        /// Updates the specified account in the repository.
        /// </summary>
        /// <param name="account">The account object containing updated information. Cannot be null.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see cref="OkObjectResult"/>
        /// with a success response and the updated account if the update is successful.  Returns <see
        /// cref="NotFoundObjectResult"/> with a failure response if the account is not found.</returns>
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

        /// <summary>
        /// Updates a range of account records in the repository.
        /// </summary>
        /// <remarks>Ensure that the provided <paramref name="accounts"/> collection is not null and
        /// contains valid account data.</remarks>
        /// <param name="accounts">The collection of <see cref="Account"/> objects to be updated. Each account must have valid data.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see cref="OkObjectResult"/>
        /// with a success response and the updated accounts if the operation succeeds.  Returns <see
        /// cref="NotFoundObjectResult"/> with a failure response if the operation fails.</returns>
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
