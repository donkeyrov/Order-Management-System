using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMgt.Model.Entities
{
    public class OrderProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderProductID { get; set; }
        [ForeignKey("Order")]
        [Required(ErrorMessage = "Please select an order")]
        [DeniedValues("0", ErrorMessage = "Please select a valid order")]
        public int OrderID { get; set; }
        [ForeignKey("Product")]
        [Required(ErrorMessage = "Please select a product")]
        [DeniedValues("0", ErrorMessage = "Please select a valid product")]
        public int ProductID { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a non-negative integer and not zero")]
        public int Quantity { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
