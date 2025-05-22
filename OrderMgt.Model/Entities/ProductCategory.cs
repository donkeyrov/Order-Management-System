using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMgt.Model.Entities
{
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
