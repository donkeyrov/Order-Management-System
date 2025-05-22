using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMgt.Model.Entities
{
    public class Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogID { get; set; }
        public DateTime LogDate { get; set; }
        public string? LogType { get; set; }        
        public string? Username { get; set; }
        public string? ComputerName { get; set; }
        public string? Source { get; set; }
        public string? Process { get; set; }
        public string    Details { get; set; }
    }
}
