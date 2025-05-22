using Microsoft.AspNetCore.Mvc;
using OrderMgt.Model.Models;

namespace OrderMgt.API.Interfaces
{
    public interface IAuthenticateService
    {
        public LoginResponseModel Authenticate(LoginModel loginModel);
    }
}
