using Microsoft.AspNetCore.Authorization;
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
    /// <summary>
    /// Provides API endpoints for managing orders, including creating, updating, retrieving, and deleting orders.
    /// </summary>
    /// <remarks>This controller handles operations related to orders, such as adding single or multiple
    /// orders,  retrieving orders by ID or criteria, processing and completing orders, and removing or updating orders.
    /// All endpoints require authorization and are accessible via the "api/OrderController" route.</remarks>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        protected readonly IOrderRepository orderRepository;             
        protected readonly IOrderService orderService;
        protected readonly ILogger<OrderController> logger;
        public OrderController(IOrderRepository _orderRepository,IOrderService _orderService , ILogger<OrderController> _logger)
        {
            orderRepository = _orderRepository;             
            orderService = _orderService;
            logger = _logger;
        }

        /// <summary>
        /// Adds a new order asynchronously.
        /// </summary>
        /// <param name="order">The order to be added. Must not be null.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see cref="OkObjectResult"/>
        /// with a success response if the order is added successfully.  Returns <see cref="NotFoundObjectResult"/> with
        /// an error response if the operation fails.</returns>
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

        /// <summary>
        /// Adds a collection of orders to the repository asynchronously.
        /// </summary>
        /// <remarks>This method attempts to add the provided orders to the repository.  Ensure that the
        /// <paramref name="orders"/> parameter contains valid <see cref="Order"/> objects.</remarks>
        /// <param name="orders">The collection of <see cref="Order"/> objects to be added. Cannot be null or empty.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see cref="OkObjectResult"/>
        /// with a success response and the added orders if the operation succeeds.  Returns <see
        /// cref="NotFoundObjectResult"/> with an error response if the operation fails.</returns>
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

        /// <summary>
        /// Finds and retrieves a collection of orders that match the specified predicate.
        /// </summary>
        /// <remarks>The <paramref name="predicate"/> parameter must be a valid serialized LINQ expression
        /// that can be deserialized into an <see cref="Expression{TDelegate}"/> of type  <see
        /// cref="Expression{Func{Order, bool}}"/>. Ensure the input string is properly formatted  to avoid
        /// deserialization errors.</remarks>
        /// <param name="predicate">A serialized string representation of a LINQ expression used to filter the orders.  The string must be
        /// properly formatted and encoded, with '@' characters replaced by '#'.</param>
        /// <returns>An <see cref="IActionResult"/> containing a <see cref="BaseResponseModel"/>.  If matching orders are found,
        /// the response's <c>Success</c> property is <see langword="true"/>  and the <c>Data</c> property contains the
        /// collection of matching orders.  If no matches are found, <c>Success</c> is <see langword="false"/> and
        /// <c>Data</c> is <see langword="null"/>.</returns>
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

        /// <summary>
        /// Processes an order with the specified identifier.
        /// </summary>
        /// <remarks>This method invokes the order processing logic through the <c>orderService</c>. 
        /// Ensure that the provided <paramref name="id"/> corresponds to a valid order in the system.</remarks>
        /// <param name="id">The unique identifier of the order to process. Must be a valid, existing order ID.</param>
        /// <returns>An <see cref="IActionResult"/> containing the result of the operation.  Returns an HTTP 200 response with a
        /// <see cref="BaseResponseModel"/> indicating success and the processed order data if the operation succeeds. 
        /// Returns an HTTP 404 response with a <see cref="BaseResponseModel"/> indicating failure and an error message
        /// if the order could not be processed.</returns>
        [HttpGet("ProcessOrderAsync")]
        public async Task<IActionResult> ProcessOrderAsync(int id)
        {
            var result = await orderService.ProcessOrder(id);

            if (result.Success)
            {
                return Ok(new BaseResponseModel() { Success = true , Data = result.Data});
            }
            else 
            {
                return NotFound(new BaseResponseModel { Success = false,ErrorMessage="Could not process the order!" });
            }
        }

        /// <summary>
        /// Completes the order with the specified identifier.
        /// </summary>
        /// <remarks>This method requires the user to be authenticated, as it uses the current user's
        /// identity to complete the order.</remarks>
        /// <param name="id">The unique identifier of the order to complete. Must be a positive integer.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see cref="OkObjectResult"/>
        /// with a success response if the order is completed successfully.  Returns <see cref="NotFoundObjectResult"/>
        /// with an error message if the order could not be completed.</returns>
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

        /// <summary>
        /// Retrieves an order by its unique identifier.
        /// </summary>
        /// <remarks>If the specified <paramref name="id"/> does not correspond to an existing order, the
        /// response may indicate an empty or null data field, depending on the implementation of the underlying
        /// repository.</remarks>
        /// <param name="id">The unique identifier of the order to retrieve. Must be a positive integer.</param>
        /// <returns>An <see cref="IActionResult"/> containing a <see cref="BaseResponseModel"/> with the order data if found.
        /// The <see cref="BaseResponseModel.Success"/> property will be <see langword="true"/> if the operation
        /// succeeds.</returns>
        [HttpGet("GetAsync")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var res = await orderRepository.GetAsync(id);
            return Ok(new BaseResponseModel { Success = true, Data = res });
        }

        /// <summary>
        /// Retrieves all orders asynchronously.
        /// </summary>
        /// <remarks>This method fetches all orders from the repository and returns them in a standardized
        /// response format.</remarks>
        /// <returns>An <see cref="IActionResult"/> containing a <see cref="BaseResponseModel"/> with a success status and the
        /// list of orders.</returns>
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var res = await orderRepository.GetAllAsync();
            return Ok(new BaseResponseModel { Success = true, Data = res });
        }

        /// <summary>
        /// Removes the specified order from the repository.
        /// </summary>
        /// <param name="order">The order to be removed. This parameter cannot be <see langword="null"/>.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see cref="OkObjectResult"/>
        /// with a success response if the order was removed successfully;  otherwise, returns <see
        /// cref="NotFoundObjectResult"/> with a failure response.</returns>
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

        /// <summary>
        /// Removes a range of orders from the repository asynchronously.
        /// </summary>
        /// <remarks>This method attempts to remove the specified orders from the repository.  If the
        /// operation is successful, a success response is returned.  If the orders cannot be found or removed, a
        /// failure response is returned.</remarks>
        /// <param name="orders">The collection of <see cref="Order"/> objects to be removed. Cannot be null or empty.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see cref="OkObjectResult"/>
        /// with a success response if the orders were removed successfully;  otherwise, returns <see
        /// cref="NotFoundObjectResult"/> with a failure response.</returns>
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

        /// <summary>
        /// Updates an existing order in the system.
        /// </summary>
        /// <param name="order">The order to update. Must contain a valid identifier and updated details.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see cref="OkObjectResult"/>
        /// with the updated order if the update is successful,  or <see cref="NotFoundObjectResult"/> if the order does
        /// not exist.</returns>
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

        /// <summary>
        /// Updates a range of orders in the system.
        /// </summary>
        /// <remarks>Ensure that the provided <paramref name="orders"/> collection contains valid and
        /// existing orders.  The method will return a success response only if all orders in the collection are
        /// successfully updated.</remarks>
        /// <param name="orders">A collection of <see cref="Order"/> objects to be updated. Each order must exist in the system.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see cref="OkObjectResult"/>
        /// with the updated orders if the operation succeeds,  or <see cref="NotFoundObjectResult"/> if the update
        /// fails.</returns>
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
