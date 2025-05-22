using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMgt.Model.Entities
{    
    public class User
    {
        public int UserID { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required(ErrorMessage = "Email is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long!")]
        [DataType(DataType.Password)]
        public string Password { get; set; } // Consider hashing the password
        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; }
        public bool IsActive { get; set; } // Indicates if the user is active or inactive
        public string Role { get; set; } // e.g., "Admin", "User", etc.
    }
}
