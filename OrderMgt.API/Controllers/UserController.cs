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
    /// Provides API endpoints for managing user-related operations, including adding, updating, retrieving, and
    /// removing users.
    /// </summary>
    /// <remarks>This controller is responsible for handling user management operations such as creating,
    /// updating, retrieving, and deleting users or collections of users. It uses dependency-injected services to
    /// interact with the user repository and handle password creation.  The controller is secured with the <see
    /// cref="AuthorizeAttribute"/>, meaning all endpoints require authentication.</remarks>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        protected readonly IUserRepository repository;
        protected readonly IRegistrationService registrationService;
        protected readonly ILogger<UserController> logger;

        public UserController(IUserRepository _userRepository,IRegistrationService _registrationService ,ILogger<UserController> _logger)
        {
            repository = _userRepository;
            registrationService = _registrationService;
            logger = _logger;
        }

        /// <summary>
        /// Adds a new user to the system asynchronously.
        /// </summary>
        /// <remarks>The user's password is processed using the registration service before being stored.
        /// Ensure the <paramref name="user"/> object is valid and contains all required properties.</remarks>
        /// <param name="user">The user to be added. The <see cref="User.Password"/> property must contain the raw password, which will be
        /// processed before storage.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see cref="OkObjectResult"/>
        /// with a <see cref="BaseResponseModel"/> containing the added user if the operation succeeds.  Returns <see
        /// cref="NotFoundObjectResult"/> with a <see cref="BaseResponseModel"/> containing an error message if the
        /// operation fails.</returns>
        [HttpPost("AddAsync")]
        public async Task<IActionResult> AddAsync(User user)
        {
            user.Password = registrationService.CreatePassword(user.Password);
            if (await repository.AddAsync(user))
            {
                return Ok(new BaseResponseModel { Success = true, Data = user });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false, ErrorMessage = "" });
            }
        }

        /// <summary>
        /// Adds a collection of users to the system asynchronously.
        /// </summary>
        /// <remarks>Each user's password is processed using the registration service before being added
        /// to the repository. Ensure that the <paramref name="users"/> collection is not null or empty to avoid
        /// unexpected behavior.</remarks>
        /// <param name="users">A collection of <see cref="User"/> objects to be added. Each user's password will be processed before being
        /// stored.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation. Returns: <list type="bullet">
        /// <item><description><see cref="OkObjectResult"/> with a <see cref="BaseResponseModel"/> containing the added
        /// users if the operation succeeds.</description></item> <item><description><see cref="NotFoundObjectResult"/>
        /// with a <see cref="BaseResponseModel"/> containing an error message if the operation
        /// fails.</description></item> </list></returns>
        [HttpPost("AddRangeAsync")]
        public async Task<IActionResult> AddRangeAsync(IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                user.Password = registrationService.CreatePassword(user.Password);
            }

            if (await repository.AddRangeAsync(users))
            {
                return Ok(new BaseResponseModel { Success = true, Data = users });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false, ErrorMessage = "" });
            }
        }

        /// <summary>
        /// Finds and retrieves a collection of users that match the specified predicate.
        /// </summary>
        /// <remarks>The <paramref name="predicate"/> string must replace the '@' character with '#' to
        /// ensure proper deserialization. This method uses the provided predicate to query the repository for matching
        /// users.</remarks>
        /// <param name="predicate">A serialized string representation of a LINQ expression that defines the criteria for filtering users. The
        /// string must use a format compatible with the <see cref="Serialize.Linq.Serializers.JsonSerializer"/>.</param>
        /// <returns>An <see cref="IActionResult"/> containing a <see cref="BaseResponseModel"/>.  If matching users are found,
        /// the response model's <c>Success</c> property is <see langword="true"/> and  the <c>Data</c> property
        /// contains the collection of users. If no matches are found, <c>Success</c> is  <see langword="false"/> and
        /// <c>Data</c> is <see langword="null"/>.</returns>
        [HttpGet("Find")]
        public IActionResult Find(string predicate)
        {
            var serializer = new ExpressionSerializer(new Serialize.Linq.Serializers.JsonSerializer());
            string serializedExpression = predicate.Replace('@', '#');

            Expression<Func<User, bool>> result = (Expression<Func<User, bool>>)serializer.DeserializeText(serializedExpression);

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
        /// Retrieves an entity by its unique identifier asynchronously.
        /// </summary>
        /// <remarks>The response includes a success flag and the retrieved entity data. If the entity is
        /// not found,  the <see cref="BaseResponseModel.Data"/> property will be null.</remarks>
        /// <param name="id">The unique identifier of the entity to retrieve. Must be a positive integer.</param>
        /// <returns>An <see cref="IActionResult"/> containing a <see cref="BaseResponseModel"/> with the operation's result. The
        /// <see cref="BaseResponseModel.Data"/> property contains the retrieved entity if found.</returns>
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
        /// Removes the specified user from the repository asynchronously.
        /// </summary>
        /// <param name="user">The user to be removed. Must not be <see langword="null"/>.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see cref="OkObjectResult"/>
        /// with a success response if the user was removed successfully;  otherwise, returns <see
        /// cref="NotFoundObjectResult"/> with a failure response.</returns>
        [HttpDelete("RemoveAsync")]
        public async Task<IActionResult> RemoveAsync(User user)
        {
            if (await repository.RemoveAsync(user))
            {
                return Ok(new BaseResponseModel { Success = true });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }

        }

        /// <summary>
        /// Removes a range of users from the repository asynchronously.
        /// </summary>
        /// <param name="users">The collection of users to be removed. Cannot be null or empty.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see cref="OkObjectResult"/>
        /// with a success response if the users were removed successfully;  otherwise, returns <see
        /// cref="NotFoundObjectResult"/> with a failure response.</returns>
        [HttpDelete("RemoveRangeAsync")]
        public async Task<IActionResult> RemoveRangeAsync(IEnumerable<User> users)
        {
            if (await repository.RemoveRangeAsync(users))
            {
                return Ok(new BaseResponseModel { Success = true });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }

        /// <summary>
        /// Updates the specified user in the repository.
        /// </summary>
        /// <param name="user">The user object containing updated information. Cannot be null.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see cref="OkObjectResult"/>
        /// with a success response and the updated user if the operation succeeds.  Returns <see
        /// cref="NotFoundObjectResult"/> with a failure response if the user could not be found.</returns>
        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(User user)
        {
            if (await repository.UpdateAsync(user))
            {
                return Ok(new BaseResponseModel { Success = true, Data = user });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }

        /// <summary>
        /// Updates a range of user records in the repository.
        /// </summary>
        /// <remarks>Ensure that the <paramref name="users"/> collection is not null and contains valid
        /// user objects.  The method relies on the repository to perform the update operation.</remarks>
        /// <param name="users">A collection of <see cref="User"/> objects to be updated. Each user must have a valid identifier.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see cref="OkObjectResult"/>
        /// with a success response and the updated users if the operation succeeds.  Returns <see
        /// cref="NotFoundObjectResult"/> with a failure response if the operation fails.</returns>
        [HttpPut("UpdateRangeAsync")]
        public async Task<IActionResult> UpdateRangeAsync(IEnumerable<User> users)
        {
            if (await repository.UpdateRangeAsync(users))
            {
                return Ok(new BaseResponseModel { Success = true, Data = users });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }
    }
}
