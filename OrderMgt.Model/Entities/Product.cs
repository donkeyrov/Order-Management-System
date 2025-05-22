using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMgt.Model.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }
        [ForeignKey("ProductCategory")]
        [Required(ErrorMessage = "Please select a product category")]
        [DeniedValues("0", ErrorMessage = "Please select a valid product category")]
        public int ProductCategoryID { get; set; }
        [Required(ErrorMessage = "Please enter a product name")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Please enter a product description")]
        public string Description { get; set; }
        [Range(0.01, float.MaxValue, ErrorMessage = "Unit price must be a positive number")]
        public float UnitPrice { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
