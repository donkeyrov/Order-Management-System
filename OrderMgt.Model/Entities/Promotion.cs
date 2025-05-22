using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMgt.Model.Entities
{
    public class Promotion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PromotionID { get; set; }
        [ForeignKey("Segment")]
        [Required(ErrorMessage = "Please select the customer segment!")]
        [DeniedValues(0, ErrorMessage = "Please select the customer segment!")]
        public int SegmentID { get; set; }        
        [Required(ErrorMessage ="Please enter the promotion name!")]
        public string? PromotionName { get; set; }
        public int MinOrders { get; set; }
        public int MaxOrders { get; set; }
        [Required(ErrorMessage ="Please enter the discount percentage!")]
        [Range(1,100,ErrorMessage ="Please enter a valid percentage!")]
        public float DiscountPercentage { get; set; }
    }
}
