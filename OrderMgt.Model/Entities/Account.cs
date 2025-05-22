using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace OrderMgt.Model.Entities
{
    /// <summary>
    /// Represents an account entity with unique identifiers, descriptive information, and metadata.
    /// </summary>
    /// <remarks>The <see cref="Account"/> class is used to store and manage information about accounts,
    /// including a unique account code, name, and description. It also tracks metadata such as the creator and the
    /// creation date. The <see cref="AccountCode"/> and <see cref="AccountName"/> properties are unique and required,
    /// ensuring that each account is distinct.</remarks>
    [Index(nameof(AccountCode), IsUnique = true)]
    [Index(nameof(AccountName), IsUnique = true)]
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountID { get; set; }        
        [MaxLength(50)]
        [Required(ErrorMessage = "Please enter the account code!")]
        public string? AccountCode { get; set; }
        [MaxLength(250)]
        [Required(ErrorMessage = "Please enter the account name!")]
        public string? AccountName { get; set; }
        [MaxLength(500)]
        [Required(ErrorMessage = "Please enter the account description!")]
        public string? Description { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
