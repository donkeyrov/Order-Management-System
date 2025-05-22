using Microsoft.AspNetCore.Authorization;
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
    /// <summary>
    /// Provides API endpoints for managing customer data, including operations such as adding, updating, retrieving,
    /// and removing customers.
    /// </summary>
    /// <remarks>This controller is designed to handle customer-related operations by interacting with the
    /// underlying customer repository. It requires authorization to access its endpoints and follows RESTful
    /// conventions for HTTP methods.</remarks>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        protected readonly ICustomerRepository repository;
        protected readonly ILogger<CustomerController> logger;

        public CustomerController(ICustomerRepository _customerRepository,ILogger<CustomerController> _logger)
        {
            repository = _customerRepository;
            logger = _logger;
        }

        /// <summary>
        /// Adds a new customer asynchronously.
        /// </summary>
        /// <param name="customer">The customer to be added. Must not be null.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see cref="OkObjectResult"/>
        /// with a success response if the customer is added successfully,  or <see cref="NotFoundObjectResult"/> with
        /// an error response if the operation fails.</returns>
        [HttpPost("AddAsync")]
        public async Task<IActionResult> AddAsync(Customer customer)
        {
            if (await repository.AddAsync(customer))
            {
                return Ok(new BaseResponseModel { Success = true, Data = customer });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false, ErrorMessage = "" });
            }
        }

        /// <summary>
        /// Adds a range of customers to the repository asynchronously.
        /// </summary>
        /// <remarks>This method attempts to add all provided customers to the repository.  Ensure that
        /// the <paramref name="customers"/> collection contains valid customer objects.</remarks>
        /// <param name="customers">A collection of <see cref="Customer"/> objects to be added.  The collection must not be null or empty.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see cref="OkObjectResult"/>
        /// with a success response and the added customers if the operation succeeds,  or <see
        /// cref="NotFoundObjectResult"/> with an error response if the operation fails.</returns>
        [HttpPost("AddRangeAsync")]
        public async Task<IActionResult> AddRangeAsync(IEnumerable<Customer> customers)
        {
            if (await repository.AddRangeAsync(customers))
            {
                return Ok(new BaseResponseModel { Success = true, Data = customers });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false, ErrorMessage = "" });
            }
        }

        /// <summary>
        /// Finds and retrieves a collection of customers that match the specified predicate.
        /// </summary>
        /// <remarks>The <paramref name="predicate"/> string must replace the '@' character with '#' to
        /// ensure proper deserialization.</remarks>
        /// <param name="predicate">A serialized string representation of a LINQ expression that defines the criteria for filtering customers.
        /// The string must use a format compatible with the <see cref="Serialize.Linq.Serializers.JsonSerializer"/>.</param>
        /// <returns>An <see cref="IActionResult"/> containing a <see cref="BaseResponseModel"/>.  If matching customers are
        /// found, the response's <c>Success</c> property is <see langword="true"/> and the <c>Data</c> property
        /// contains the results. If no matches are found, <c>Success</c> is <see langword="false"/> and <c>Data</c> is
        /// <see langword="null"/>.</returns>
        [HttpGet("Find")]
        public IActionResult Find(string predicate)
        {
            var serializer = new ExpressionSerializer(new Serialize.Linq.Serializers.JsonSerializer());
            string serializedExpression = predicate.Replace('@', '#');

            Expression<Func<Customer, bool>> result = (Expression<Func<Customer, bool>>)serializer.DeserializeText(serializedExpression);

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
        /// Retrieves an item by its unique identifier asynchronously.
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
        /// Retrieves all items asynchronously.
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
        /// Removes the specified customer from the repository asynchronously.
        /// </summary>
        /// <param name="customer">The customer to be removed. Must not be <see langword="null"/>.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see cref="OkObjectResult"/>
        /// with a success response if the customer was removed successfully;  otherwise, returns <see
        /// cref="NotFoundObjectResult"/> with a failure response.</returns>
        [HttpDelete("RemoveAsync")]
        public async Task<IActionResult> RemoveAsync(Customer customer)
        {
            if (await repository.RemoveAsync(customer))
            {
                return Ok(new BaseResponseModel { Success = true });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }

        }

        /// <summary>
        /// Removes a range of customers from the repository.
        /// </summary>
        /// <remarks>Ensure that the provided <paramref name="customers"/> collection is not null or
        /// empty, and that all customers exist in the repository.</remarks>
        /// <param name="customers">The collection of customers to be removed. Each customer must exist in the repository.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see cref="OkObjectResult"/>
        /// with a success response if the customers were removed successfully;  otherwise, returns <see
        /// cref="NotFoundObjectResult"/> with a failure response if the operation failed.</returns>
        [HttpDelete("RemoveRangeAsync")]
        public async Task<IActionResult> RemoveRangeAsync(IEnumerable<Customer> customers)
        {
            if (await repository.RemoveRangeAsync(customers))
            {
                return Ok(new BaseResponseModel { Success = true });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }

        /// <summary>
        /// Updates the specified customer in the repository.
        /// </summary>
        /// <param name="customer">The customer object containing updated information. Cannot be null.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see cref="OkObjectResult"/>
        /// with a success response and the updated customer if the update is successful.  Returns <see
        /// cref="NotFoundObjectResult"/> with a failure response if the customer is not found.</returns>
        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(Customer customer)
        {
            if (await repository.UpdateAsync(customer))
            {
                return Ok(new BaseResponseModel { Success = true, Data = customer });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }

        /// <summary>
        /// Updates a range of customer records in the repository.
        /// </summary>
        /// <remarks>Ensure that each customer in the provided collection exists in the repository before
        /// calling this method.</remarks>
        /// <param name="customers">The collection of <see cref="Customer"/> objects to update. Each object must represent an existing customer.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see cref="OkObjectResult"/>
        /// with the updated customers if the operation succeeds,  or <see cref="NotFoundObjectResult"/> if the update
        /// fails.</returns>
        [HttpPut("UpdateRangeAsync")]
        public async Task<IActionResult> UpdateRangeAsync(IEnumerable<Customer> customers)
        {
            if (await repository.UpdateRangeAsync(customers))
            {
                return Ok(new BaseResponseModel { Success = true, Data = customers });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }
    }
}
