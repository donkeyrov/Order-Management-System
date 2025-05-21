using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMgt.Model.Entities
{
    public class OrderStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderStatusID { get; set; }
        [Required(ErrorMessage = "Please enter a status name")]
        public string StatusName { get; set; } // e.g., "Pending", "Completed", "Cancelled"
        [Required(ErrorMessage = "Please enter a status description")]
        public string Description { get; set; } // e.g., "Order is pending", "Order has been completed"
        public int Ordering { get; set; } //used for ordering the statuses
        public string CreatedBy { get; set; } 
        public DateTime DateCreated { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
