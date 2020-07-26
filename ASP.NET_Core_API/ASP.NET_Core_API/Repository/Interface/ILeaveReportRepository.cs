using ASP.NET_Core_API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core_API.Repository.Interface
{
    public interface ILeaveReportRepository
    {
        Task<IEnumerable<LeaveReportVM>> GetAll();
        LeaveReportVM Get(int Id);
        int Create(LeaveReportVM leaveReportVM);
        int Update(LeaveReportVM leaveReportVM, int Id);
        int Delete(int Id);
    }
}
