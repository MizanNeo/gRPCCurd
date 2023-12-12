using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NeoSOFT.Application.Contracts;
using NeoSOFT.Domain.Models;

namespace NeoSOFT.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    public class DepartmentController:AnonymousBaseController
    {
        protected IDepartmentService _deparmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _deparmentService = departmentService;
        }

        /// <summary>
        /// Get All Departments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetAll))]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _deparmentService.GetAll());
        }

        /// <summary>
        /// Get Department By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetById))]
        public async Task<IActionResult> GetById([FromQuery] int Id)
        {
            return Ok(await _deparmentService.GetById(Id));
        }

        /// <summary>
        /// Create Department
        /// </summary>
        /// <param name="departMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(Create))]
        public async Task<IActionResult> Create([FromBody] DepartMaster departMaster)
        {
            return Ok(await _deparmentService.Create(departMaster));
        }

        /// <summary>
        /// Update Department
        /// </summary>
        /// <param name="departMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(Update))]
        public async Task<IActionResult> Update([FromBody] DepartMaster departMaster)
        {
            return Ok(await _deparmentService.Update(departMaster));
        }
        /// <summary>
        /// Delete Department
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route(nameof(Delete))]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            return Ok(await _deparmentService.Delete(id));
        }
    }
}
