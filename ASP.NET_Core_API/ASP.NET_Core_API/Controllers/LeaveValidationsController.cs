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
    public class LeaveValidationsController : ControllerBase
    {
        ILeaveValidationRepository _leaveValidationRepository;
        public LeaveValidationsController(ILeaveValidationRepository leaveValidationRepository)
        {
            _leaveValidationRepository = leaveValidationRepository;
        }

        [HttpGet]
        public IEnumerable<LeaveValidationVM> GetLeaveValidation()
        {
            return _leaveValidationRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<LeaveValidationVM>> GetLeaveValidations(int Id)
        {
            return await _leaveValidationRepository.Get(Id);
        }

        [HttpPost]
        public IActionResult CreateLeaveValidation(LeaveValidationVM leaveValidation)
        {
            var create = _leaveValidationRepository.Create(leaveValidation);
            if (create > 0)
            {
                return Ok(create);
            }
            return BadRequest("Failed to created data!");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLeaveValidation(LeaveValidationVM leaveValidation, int Id)
        {
            var update = _leaveValidationRepository.Update(leaveValidation, Id);
            if (update > 0)
            {
                return Ok(update);
            }
            return BadRequest("Failed to updated data!");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLeaveValidation(int Id)
        {
            var delete = _leaveValidationRepository.Delete(Id);
            if (delete > 0)
            {
                return Ok(delete);
            }
            return BadRequest("Failed to delete data!");
        }
    }
}
