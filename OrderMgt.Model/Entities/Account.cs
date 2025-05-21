using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace OrderMgt.Model.Entities
{
    [Index(nameof(AccountCode), IsUnique = true)]
    [Index(nameof(AccountName), IsUnique = true)]
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountID { get; set; }        
        [MaxLength(50)]
        [Required(ErrorMessage = "Please enter the account code!")]
        public string AccountCode { get; set; }
        [MaxLength(250)]
        [Required(ErrorMessage = "Please enter the account name!")]
        public string AccountName { get; set; }
        [MaxLength(500)]
        [Required(ErrorMessage = "Please enter the account description!")]
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
