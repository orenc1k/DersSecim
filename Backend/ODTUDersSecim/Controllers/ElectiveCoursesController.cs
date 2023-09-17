

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ODTUDersSecim.Services;
using Microsoft.AspNetCore.Mvc;
using ODTUDersSecim.Models;
using System.Net;
using ODTUDersSecim.DTOs;

namespace ODTUDersSecim.Controllers
{
    [Route("api/[controller]/[action]/")]
    [ApiController]
    public class ElectiveCoursesController : Controller
    {

        private readonly  ElectiveCoursesService _electiveCoursesService;

        public ElectiveCoursesController(ElectiveCoursesService electiveCoursesService)
        {
            _electiveCoursesService = electiveCoursesService;
        }


    
        [HttpGet]
        [ProducesResponseType(typeof(ElectiveCourses), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ElectiveCourses), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(List<ElectiveCourses>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetElectiveCourses()
        {
            var electiveCourses = await _electiveCoursesService.GetElectiveCourses();
            return Ok(electiveCourses);
        }

        [HttpGet("{electiveCoursesId}")]
        [ProducesResponseType(typeof(ElectiveCourses), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ElectiveCourses), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetElectiveCourse(int electiveCoursesId)
        {
            var electiveCourse = await _electiveCoursesService.GetElectivecourse(electiveCoursesId);
            if (electiveCourse == null)
            {
                return NotFound();
            }
            return Ok(electiveCourse);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ElectiveCourses), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ElectiveCourses), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> AddElectiveCourse(ElectiveCoursesDTO electiveCourseDTO)
        {
            var result = await _electiveCoursesService.AddElectiveCourse(electiveCourseDTO);
            if (result== null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ElectiveCourses), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ElectiveCourses), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateElectiveCourse(ElectiveCoursesDTO electiveCourse)
        {
            var result = await _electiveCoursesService.UpdateElectiveCourse(electiveCourse);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{electiveCoursesId}")]
        [ProducesResponseType(typeof(ElectiveCourses), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ElectiveCourses), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteElectiveCourse(int electiveCoursesId)
        {
            var result = await _electiveCoursesService.DeleteElectiveCourse(electiveCoursesId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    
    }

}    