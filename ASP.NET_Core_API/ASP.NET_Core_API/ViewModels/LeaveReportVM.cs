using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core_API.ViewModels
{
    public class LeaveReportVM
    {
        public int Id { get; set; }
        public DateTime ApplicationEntry { get; set; }
        public int LeaveValidationId { get; set; }
        public int HumanResourceId { get; set; }
        public string Action { get; set; }
        public int DurationLV { get; set; }
    }
}
