using ASP.NET_Core_API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core_API.Repository.Interface
{
    public interface IEmployeeRepository
    {
        IEnumerable<EmployeeVM> GetAll();
        Task<IEnumerable<EmployeeVM>> Get(int Id);
        int Create(EmployeeVM employeeVM);
        int Update(EmployeeVM employeeVM, int Id);
        int Delete(int Id);
    }
}
