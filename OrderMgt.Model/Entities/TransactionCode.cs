using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderMgt.Model.Entities
{
    /// <summary>
    /// Represents a transaction code used to categorize and process financial transactions.
    /// </summary>
    /// <remarks>A transaction code typically includes a unique identifier, a code string, and a description. 
    /// It also specifies the associated debit and credit accounts, as well as metadata such as  creation details and
    /// whether the transaction code is active. The <see cref="Code"/> property  is unique and required, ensuring that
    /// each transaction code is distinct.</remarks>
    [Index(nameof(Code), IsUnique = true)]
    public class TransactionCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionCodeID { get; set; }
        [Required(ErrorMessage = "Code is required")]
        public string? Code { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }         
        public int DebitAccountID { get; set; }        
        public int CreditAccountID { get; set; }
        public bool Active { get; set; }     
        public string? CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        
    }
}
