using ASP.NET_Core_API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core_API.Repository.Interface
{
    public interface ILeaveValidationRepository
    {
        Task<IEnumerable<LeaveValidationVM>> GetAll();
        LeaveValidationVM Get(int Id);
        int Create(LeaveValidationVM leaveValidationVM);
        int Update(LeaveValidationVM leaveValidationVM, int Id);
        int Delete(int Id);
    }
}
