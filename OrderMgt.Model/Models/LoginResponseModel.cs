using OrderMgt.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMgt.Model.Models
{
    public class LoginResponseModel
    {
        public string? token { get; set; }
        public DateTime? expiresAt { get; set; }
        public User? user { get; set; }
    }
}
