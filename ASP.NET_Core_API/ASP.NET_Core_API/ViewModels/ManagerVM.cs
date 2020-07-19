using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core_API.ViewModels
{
    public class ManagerVM
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public int Nip { get; set; }
        public string Division { get; set; }
    }
}
