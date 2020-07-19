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
    public class LeaveReportsController : ControllerBase
    {
        ILeaveReportRepository _leaveReportRepository;
        public LeaveReportsController(ILeaveReportRepository leaveReportRepository)
        {
            _leaveReportRepository = leaveReportRepository;
        }

        [HttpGet]
        public IEnumerable<LeaveReportVM> GetLeaveReport()
        {
            return _leaveReportRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<LeaveReportVM>> GetLeaveReports(int Id)
        {
            return await _leaveReportRepository.Get(Id);
        }

        [HttpPost]
        public IActionResult CreateLeaveReport(LeaveReportVM leaveReport)
        {
            var create = _leaveReportRepository.Create(leaveReport);
            if (create > 0)
            {
                return Ok(create);
            }
            return BadRequest("Failed to created data!");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLeaveReport(LeaveReportVM leaveReport, int Id)
        {
            var update = _leaveReportRepository.Update(leaveReport, Id);
            if (update > 0)
            {
                return Ok(update);
            }
            return BadRequest("Failed to updated data!");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLeaveReport(int Id)
        {
            var delete = _leaveReportRepository.Delete(Id);
            if (delete > 0)
            {
                return Ok(delete);
            }
            return BadRequest("Failed to delete data!");
        }
    }
}
