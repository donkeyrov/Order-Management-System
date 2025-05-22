using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OrderMgt.API.Interfaces;
using OrderMgt.Model.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrderMgt.API.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        protected readonly IConfiguration configuration;
        protected readonly ILogger<AuthenticateService> logger;
        protected readonly IUserRepository userRepository;
        protected readonly IRegistrationService registrationService;
        public AuthenticateService(IConfiguration _configuration, ILogger<AuthenticateService> _logger, IUserRepository _userRepository, IRegistrationService _registrationService)
        {
            configuration = _configuration;
            logger = _logger;
            userRepository = _userRepository;
            registrationService = _registrationService;
        }

        public LoginResponseModel Authenticate(LoginModel loginModel)
        {
            //check for valid user login details
            var user = userRepository.Find(x => x.Username == loginModel.Username
                                    && x.Password == registrationService.CreatePassword(loginModel.Password)
                                    && x.IsActive == true
                                    )
                .FirstOrDefault();

            if (user == null)
            {
                return null;
            }

            var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, loginModel.Username),
                    new Claim(ClaimTypes.NameIdentifier, loginModel.Username),
                    new Claim(ClaimTypes.Authentication , CookieAuthenticationDefaults.AuthenticationScheme),
                    new Claim(ClaimTypes.Role, "Admin"),
                },
                 "OrderMgtSystem"
                 );

            identity.AddClaims(new List<Claim>
                {                   
                    new Claim("UserId", user.UserID.ToString()),
                    new Claim("Username", user.Username),
                    new Claim(ClaimTypes.Email, user.Username),
                    new Claim(ClaimTypes.Version, "V3.1"),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(JwtRegisteredClaimNames.Sub, "1"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }
            );

            //create jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(configuration["JWT:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = configuration["JWT:ValidIssuer"],
                Audience = configuration["JWT:ValidAudience"],
                Subject = new ClaimsIdentity(identity),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            LoginResponseModel loginResponseModel
                = new LoginResponseModel()
                {
                    token = tokenHandler.WriteToken(token),
                    expiresAt = DateTime.UtcNow.AddHours(1),
                    user = user
                };

            return loginResponseModel;
        }
    }
}
