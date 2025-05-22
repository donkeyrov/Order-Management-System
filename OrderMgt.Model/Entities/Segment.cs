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
    /// Represents a segment entity with an identifier, name, description, creator, and creation date.
    /// </summary>
    /// <remarks>This class is typically used to define and manage segments in the application. Each segment
    /// has a unique identifier and includes metadata such as its name, description, the user who created it, and the
    /// date it was created.</remarks>
    public class Segment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SegmentID { get; set; }
        [Required(ErrorMessage = "Please enter a segment name")]
        public string? SegmentName { get; set; }
        [Required(ErrorMessage = "Please enter a segment description")]
        public string? Description { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
