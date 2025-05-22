using Microsoft.EntityFrameworkCore;
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
    /// Represents a customer in the system, including personal details, contact information,  and associated metadata
    /// such as customer segment and creation details.
    /// </summary>
    /// <remarks>This class is used to store and manage customer information, including unique identifiers 
    /// for email and phone number. It is designed to ensure data integrity through validation  attributes and database
    /// constraints.</remarks>
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(PhoneNumber), IsUnique = true)]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }
        [Required(ErrorMessage = "Please enter a first name")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Please enter a last name")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Please enter an email address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Please enter a phone number")]
        [DataType(DataType.PhoneNumber,ErrorMessage = "Enter a valid phone number")]
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        [ForeignKey("Segment")]
        [Required(ErrorMessage = "Please select a customer segment")]
        [DeniedValues("0", ErrorMessage = "Please select a valid product category")]
        public int SegmentID { get; set; }
        public string?   CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
