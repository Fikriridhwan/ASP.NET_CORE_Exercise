using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.NET_Core_API.Repository.Interface;
using ASP.NET_Core_API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagersController : ControllerBase
    {
        IManagerRepository _managerRepository;
        public ManagersController(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }

        [HttpGet]
        public IEnumerable<ManagerVM> GetManager()
        {
            return _managerRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<ManagerVM>> GetManagers(int Id)
        {
            return await _managerRepository.Get(Id);
        }

        [HttpPost]
        public IActionResult CreateManager(ManagerVM manager)
        {
            var create = _managerRepository.Create(manager);
            if (create > 0)
            {
                return Ok(create);
            }
            return BadRequest("Failed to created data!");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateManager(ManagerVM manager, int Id)
        {
            var update = _managerRepository.Update(manager, Id);
            if (update > 0)
            {
                return Ok(update);
            }
            return BadRequest("Failed to updated data!");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteManager(int Id)
        {
            var delete = _managerRepository.Delete(Id);
            if (delete > 0)
            {
                return Ok(delete);
            }
            return BadRequest("Failed to delete data!");
        }
    }
}
