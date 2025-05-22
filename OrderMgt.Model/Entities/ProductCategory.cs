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
    /// Represents a category of products in the system, including its name, description, and metadata.
    /// </summary>
    /// <remarks>This class is typically used to group products into logical categories for organizational
    /// purposes. Each category includes a unique identifier, a name, a description, and metadata about its
    /// creation.</remarks>
    public class ProductCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductCategoryID { get; set; }
        [Required(ErrorMessage = "Please enter a product category name")]
        public string? CategoryName { get; set; }
        [Required(ErrorMessage = "Please enter a product category description")]
        public string? Description { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
