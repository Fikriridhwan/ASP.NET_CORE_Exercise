using ASP.NET_Core_API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core_API.Repository.Interface
{
    public interface IHumanResourceRepository
    {
        Task<IEnumerable<HumanResourceVM>> GetAll();
        HumanResourceVM Get(int Id);
        int Create(HumanResourceVM humanResourceVM);
        int Update(HumanResourceVM humanResourceVM, int Id);
        int Delete(int Id);
    }

}
