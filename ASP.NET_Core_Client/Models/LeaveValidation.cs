using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core_Client.Models
{
    public class LeaveValidation
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public int ValidDuration { get; set; }
        public int ManagerId { get; set; }
        public int LeaveApplicationId { get; set; }
        public string Reason { get; set; }
        public int Duration { get; set; }
        public int Nip { get; set; }
        public string Name { get; set; }
    }
}
