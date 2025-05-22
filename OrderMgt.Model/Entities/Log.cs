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
    /// Represents a log entry that records details about an event or action within the system.
    /// </summary>
    /// <remarks>This class is typically used to store and retrieve information about system events, 
    /// including metadata such as the date, type, user, and source of the event. Each log entry  is uniquely identified
    /// by its <see cref="LogID"/>.</remarks>
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
