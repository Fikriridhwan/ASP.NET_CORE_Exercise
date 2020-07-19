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
    public class HumanResourcesController : ControllerBase
    {
        IHumanResourceRepository _humanResourceRepository;
        public HumanResourcesController(IHumanResourceRepository humanResourceRepository)
        {
            _humanResourceRepository = humanResourceRepository;
        }

        [HttpGet]
        public IEnumerable<HumanResourceVM> GetHumanResource()
        {
            return _humanResourceRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<HumanResourceVM>> GetHumanResource(int Id)
        {
            return await _humanResourceRepository.Get(Id);
        }

        [HttpPost]
        public IActionResult CreateHumanResource(HumanResourceVM humanResource)
        {
            var create = _humanResourceRepository.Create(humanResource);
            if (create > 0)
            {
                return Ok(create);
            }
            return BadRequest("Failed to created data!");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateHumanResource(HumanResourceVM humanResource, int Id)
        {
            var update = _humanResourceRepository.Update(humanResource, Id);
            if (update > 0)
            {
                return Ok(update);
            }
            return BadRequest("Failed to updated data!");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteHumanResource(int Id)
        {
            var delete = _humanResourceRepository.Delete(Id);
            if (delete > 0)
            {
                return Ok(delete);
            }
            return BadRequest("Failed to delete data!");
        }
    }
}
