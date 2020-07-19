using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core_API.Models
{
    [Table("TB_R_LeaveApplication")]
    public class LeaveApplication
    {
        [Key]
        public int Id { get; set; }
        public string Reason { get; set; }
        public int LeaveDuration { get; set; }
        public Employee Employee { get; set; }
    }
}
