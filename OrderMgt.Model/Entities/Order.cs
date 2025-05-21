using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMgt.Model.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }
        public DateTime TransactionDate { get; set; }
        [Required(ErrorMessage = "Please enter an order number")]
        public string OrderNo { get; set; }
        [ForeignKey("Customer")]
        [Required(ErrorMessage = "Please select a customer")]
        [DeniedValues("0", ErrorMessage = "Please select a valid customer")]
        public int CustomerID { get; set; }        
        public string Details { get; set; }
        [ForeignKey("OrderStatus")]
        [Required(ErrorMessage = "Please select an order status")]
        [DeniedValues("0", ErrorMessage = "Please select a valid order status")]
        public int OrderStatusID { get; set; }
        [Range(0.01, float.MaxValue, ErrorMessage = "Total must be a positive number")]
        public float Total { get; set; }        
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string CompletedBy { get; set; }
        public DateTime DateCompleted { get; set; }

        public Product Product { get; set; }
        public Customer Customer { get; set; }
        public OrderStatus OrderStatus { get; set; }

    }
}
