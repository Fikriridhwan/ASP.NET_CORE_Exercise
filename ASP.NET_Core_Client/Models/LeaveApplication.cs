using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core_Client.Models
{
    public class LeaveApplication
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public int LeaveDuration { get; set; }
        public int Nip { get; set; }
        public string EmployeeName { get; set; }
        public int EmployeeId { get; set; }
    }
}
