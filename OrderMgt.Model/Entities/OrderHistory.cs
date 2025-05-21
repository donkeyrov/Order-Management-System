using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMgt.Model.Entities
{
    public class OrderHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderHistoryID { get; set; }
        public int OrderID { get; set; }
        public DateTime TransactionDate { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public string Details { get; set; }
        public int OrderStatusID { get; set; }
        public float Total { get; set; }        
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string CompletedBy { get; set; }
        public DateTime DateCompleted { get; set; }
       
    }
}
