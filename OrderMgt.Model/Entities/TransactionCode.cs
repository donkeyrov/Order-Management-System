using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderMgt.Model.Entities
{
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
