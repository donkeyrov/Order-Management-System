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
    /// Represents a financial transaction, including details such as debit, credit, balance, and associated metadata.
    /// </summary>
    /// <remarks>This class is used to record and manage financial transactions. Each transaction is uniquely
    /// identified by  <see cref="TransactionID"/> and includes information such as the transaction date, type, amounts
    /// (debit, credit, balance),  and associated references (e.g., order, customer, and user).   The <see
    /// cref="TransactionCodeID"/> and <see cref="CustomerID"/> properties must be valid, non-zero values, as enforced 
    /// by the associated validation attributes.</remarks>
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionID { get; set; }
        public DateTime TransactionDate { get; set; }
        [ForeignKey("TransactionCode")]
        [DeniedValues("0", ErrorMessage = "Please select a valid transactionCode")]
        public int TransactionCodeID { get; set; }
        public string? TransactionType { get; set; }
        public float Debit { get; set; }
        public float Credit { get; set; }
        public float Balance { get; set; }
        public string? Description { get; set; }
        public string? Reference { get; set; }        
        public int OrderID { get; set; }  
        public float TaxAmount { get; set; }
        [ForeignKey("Customer")]
        [DeniedValues("0", ErrorMessage = "Please select a valid customer")]
        public int CustomerID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
    }
}
