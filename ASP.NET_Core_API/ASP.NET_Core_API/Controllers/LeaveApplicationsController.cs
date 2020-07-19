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
    public class LeaveApplicationsController : ControllerBase
    {
        ILeaveApplicationRepository _leaveApplicationRepository;
        public LeaveApplicationsController(ILeaveApplicationRepository leaveApplicationRepository)
        {
            _leaveApplicationRepository = leaveApplicationRepository;
        }

        [HttpGet]
        public IEnumerable<LeaveApplicationVM> GetLeaveApplication()
        {
            return _leaveApplicationRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<LeaveApplicationVM>> GetLeaveApplications(int Id)
        {
            return await _leaveApplicationRepository.Get(Id);
        }

        [HttpPost]
        public IActionResult CreateLeaveApplication(LeaveApplicationVM leaveApplication)
        {
            var create = _leaveApplicationRepository.Create(leaveApplication);
            if (create > 0)
            {
                return Ok(create);
            }
            return BadRequest("Failed to created data!");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLeaveApplication(LeaveApplicationVM leaveApplication, int Id)
        {
            var update = _leaveApplicationRepository.Update(leaveApplication, Id);
            if (update > 0)
            {
                return Ok(update);
            }
            return BadRequest("Failed to updated data!");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLeaveApplication(int Id)
        {
            var delete = _leaveApplicationRepository.Delete(Id);
            if (delete > 0)
            {
                return Ok(delete);
            }
            return BadRequest("Failed to delete data!");
        }
    }
}
