using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core_API.Models
{
    [Table("TB_R_LeaveValidation")]
    public class LeaveValidation
    {
        [Key]
        public int Id { get; set; }
        public string Action { get; set; }
        public int ValidDuration { get; set; }
        public Manager Manager { get; set; }
        public LeaveApplication LeaveApplication { get; set; }
    }
}
