using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMgt.Model.Entities
{
    public class Segment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SegmentID { get; set; }
        [Required(ErrorMessage = "Please enter a segment name")]
        public string SegmentName { get; set; }
        [Required(ErrorMessage = "Please enter a segment description")]
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }
}
