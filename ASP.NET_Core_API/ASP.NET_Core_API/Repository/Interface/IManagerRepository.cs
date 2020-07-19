using ASP.NET_Core_API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core_API.Repository.Interface
{
    public interface IManagerRepository
    {
        IEnumerable<ManagerVM> GetAll();
        Task<IEnumerable<ManagerVM>> Get(int Id);
        int Create(ManagerVM managerVM);
        int Update(ManagerVM managerVM, int Id);
        int Delete(int Id);
    }
}
