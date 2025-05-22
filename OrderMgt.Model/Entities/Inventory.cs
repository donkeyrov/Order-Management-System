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
    /// Represents an inventory record, including details about the product, quantity, and metadata.
    /// </summary>
    /// <remarks>This class is typically used to track the stock levels of products in an inventory system. 
    /// Each inventory record is uniquely identified by the <see cref="InventoryID"/> property.</remarks>
    public class Inventory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InventoryID { get; set; }
        [Required(ErrorMessage = "Product ID is required")]
        [DeniedValues("0", ErrorMessage = "Please select a valid product")]
        public int ProductID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative integer")]
        public int Quantity { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
