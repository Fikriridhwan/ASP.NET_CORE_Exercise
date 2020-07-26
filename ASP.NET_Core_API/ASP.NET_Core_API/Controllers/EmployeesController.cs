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
    public class EmployeesController : ControllerBase
    {
        IEmployeeRepository _employeeRepository;
        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<EmployeeVM>> GetEmployees()
        {
            return await _employeeRepository.GetAll();
        }

        [HttpGet("{id}")]
        public EmployeeVM GetEmployees(int Id)
        {
            return _employeeRepository.Get(Id);
        }

        [HttpPost]
        public IActionResult CreateEmployee(EmployeeVM employee)
        {
            var create = _employeeRepository.Create(employee);
            if (create > 0)
            {
                return Ok(create);
            }
            return BadRequest("Failed to created data!");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(EmployeeVM employee, int Id)
        {
            var update = _employeeRepository.Update(employee, Id);
            if (update > 0)
            {
                return Ok(update);
            }
            return BadRequest("Failed to updated data!");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int Id)
        {
            var delete = _employeeRepository.Delete(Id);
            if (delete > 0)
            {
                return Ok(delete);
            }
            return BadRequest("Failed to delete data!");
        }

        [HttpPost("Login")]
        public IActionResult LoginEmployee(EmployeeVM employee)
        {
            int res = _employeeRepository.Login(employee);
            if (res == 1)
            {
                return Ok(res);
            }
            return BadRequest("Login failed, username or password not match");
        }
    }
}
