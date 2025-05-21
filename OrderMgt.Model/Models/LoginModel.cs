using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMgt.Model.Models
{
    public class LoginModel
    {
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid email address")]
        [Required(ErrorMessage = "Please enter your email address!")]
        public string Username { get; set; }
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long!")]
        [Required(ErrorMessage = "Please enter a password for your account!")]
        public string Password { get; set; }
    }
}
