using ASP.NET_Core_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core_API.Context
{
    public class apiContext : DbContext
    {
        public apiContext(DbContextOptions<apiContext> options) : base(options){}
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<LeaveApplication> LeaveApplications { get; set; }
        public DbSet<LeaveValidation> LeaveValidations { get; set; }
        public DbSet<HumanResource> HumanResources { get; set; }
        public DbSet<LeaveReport> LeaveReports { get; set; }
    }
}
