using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ODTUDersSecim.Services;
using Microsoft.AspNetCore.Mvc;
using ODTUDersSecim.Models;
using System.Net;

namespace ODTUDersSecim.Controllers
{
    [Route("api/[controller]/[action]/")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly DepartmentService _departmentService;

        public DepartmentController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Departments), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Departments), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(List<Departments>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDepartments()
        {
            var subjects = await _departmentService.GetDepartmentsAsync();
            return Ok(subjects);
        }

        [HttpGet("{deptCode}")]
        [ProducesResponseType(typeof(Departments), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Departments), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetDepartment(int deptCode)
        {
            var subject = await _departmentService.GetDepartment(deptCode);
            if (subject == null)
            {
                return NotFound();
            }
            return Ok(subject);
        }

       
        [HttpGet("{deptName}")]
        [ProducesResponseType(typeof(Departments), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Departments), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult?> GetDepartmentCode(string deptName)
        {
            var subject = await _departmentService.GetDepartmentCode(deptName);
            if (subject == null)
            {
                return NotFound();
            }
            return Ok(subject);
        }
        [HttpPost]
        [ProducesResponseType(typeof(Departments), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Departments), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> AddDepartment(Departments department)
        {
            var addedSubject = await _departmentService.AddDepartment(department);
            return Ok(addedSubject);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Departments), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Departments), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateDepartment(Departments department)
        {
            var updatedSubject = await _departmentService.UpdateDepartment(department);
            if (updatedSubject == null)
            {
                return NotFound();
            }
            return Ok(updatedSubject);
        }

        [HttpDelete("{deptCode}")]
        [ProducesResponseType(typeof(Departments), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Departments), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteDepartment(int deptCode)
        {
            var deletedSubject = await _departmentService.DeleteDepartment(deptCode);
            if (deletedSubject == null)
            {
                return NotFound();
            }
            return Ok(deletedSubject);
        }
    }
}

