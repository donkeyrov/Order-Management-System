using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMgt.Model.Entities
{
    /// <summary>
    /// Represents the historical record of an order, including details about its status,  associated customer,
    /// transaction dates, and financial information.
    /// </summary>
    /// <remarks>This class is typically used to track the lifecycle of an order, including its creation, 
    /// completion, and any associated metadata such as discounts and totals. It is designed to  provide a comprehensive
    /// view of an order's history for reporting or auditing purposes.</remarks>
    public class OrderHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderHistoryID { get; set; }
        public int OrderID { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? OrderNo { get; set; }
        public int CustomerID { get; set; } 
        public string? Details { get; set; }
        public string? OrderStatus { get; set; }
        public float Discount { get; set; }
        public float Total { get; set; }        
        public string? CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string? CompletedBy { get; set; }
        public DateTime DateCompleted { get; set; }
       
    }
}
