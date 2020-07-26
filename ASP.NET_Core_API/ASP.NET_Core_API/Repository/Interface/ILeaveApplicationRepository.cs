using ASP.NET_Core_API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core_API.Repository.Interface
{
    public interface ILeaveApplicationRepository
    {
        Task<IEnumerable<LeaveApplicationVM>> GetAll();
       LeaveApplicationVM Get(int Id);
        int Create(LeaveApplicationVM leaveApplicationVM);
        int Update(LeaveApplicationVM leaveApplicationVM, int Id);
        int Delete(int Id);
    }
}
