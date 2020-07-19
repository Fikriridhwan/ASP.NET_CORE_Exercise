using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core_API.Models
{
    [Table("TB_R_LeaveReport")]
    public class LeaveReport
    {
        [Key]
        public int Id { get; set; }
        public DateTime ApplicationEntry { get; set; }
        public LeaveValidation LeaveValidation { get; set; }
        public HumanResource HumanResource { get; set; }

    }
}
