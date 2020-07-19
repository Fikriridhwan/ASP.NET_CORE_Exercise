using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core_Client.Models
{
    public class Employee
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Nip { get; set; }
        public int AnnualLeaveRemaining { get; set; }
    }
}
