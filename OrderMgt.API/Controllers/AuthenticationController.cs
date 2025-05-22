using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderMgt.API.Interfaces;
using OrderMgt.Model.Models;

namespace OrderMgt.API.Controllers
{
    /// <summary>
    /// Provides endpoints for user authentication, including login functionality.
    /// </summary>
    /// <remarks>This controller handles user authentication requests, such as validating login credentials 
    /// and generating JWT tokens for authorized access. All endpoints in this controller are  accessible without
    /// authentication, as indicated by the <see cref="AllowAnonymousAttribute"/>.</remarks>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        protected readonly IAuthenticateService authenticateService;
        protected readonly ILogger<AuthenticationController> logger;

        public AuthenticationController(IAuthenticateService _authenticateService, ILogger<AuthenticationController> _logger)
        {
            authenticateService = _authenticateService;
            logger = _logger;
        }

       
        /// <summary>
        /// Authenticates a user with the provided login credentials and returns a JWT token if successful.
        /// </summary>       
        /// <param name="loginModel"></param>
        /// <returns>Jwt Token</returns>
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponseModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var loginResponse = authenticateService.Authenticate(loginModel);

            if (loginResponse == null)
            {
                return BadRequest(new { message = "Username or Password is incorrect.." });
            }

            return Ok(loginResponse);
        }
    }
}
