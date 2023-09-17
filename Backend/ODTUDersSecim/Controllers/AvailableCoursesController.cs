

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
    public class AvailableCoursesController : Controller
    {

        private readonly AvailableCoursesService _availableCoursesService;

        public AvailableCoursesController(AvailableCoursesService availableCoursesService)
        {
            _availableCoursesService = availableCoursesService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(AvailableCourses), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AvailableCourses), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(List<AvailableCourses>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAvailableCourses()
        {
            var availableCourses = await _availableCoursesService.GetAvailableCourses();
            return Ok(availableCourses);
        }

        [HttpGet("{availableCoursesId}")]
        [ProducesResponseType(typeof(AvailableCourses), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AvailableCourses), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAvailableCourse(int availableCoursesId)
        {
            var availableCourse = await _availableCoursesService.GetAvailablecourse(availableCoursesId);
            if (availableCourse == null)
            {
                return NotFound();
            }
            return Ok(availableCourse);
        }

        [HttpPost]
        [ProducesResponseType(typeof(AvailableCourses), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AvailableCourses), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> AddAvailableCourse(AvailableCoursesDTO availableCoursesDTO)
        {
            var result = await _availableCoursesService.AddAvailableCourse(availableCoursesDTO);
            if (result== null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(AvailableCourses), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AvailableCourses), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateAvailableCourse(AvailableCoursesDTO availableCourse)
        {
            var result = await _availableCoursesService.UpdateAvailableCourse(availableCourse);
            if (result==null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{availableCoursesId}")]
        [ProducesResponseType(typeof(AvailableCourses), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AvailableCourses), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAvailableCourse(int availableCoursesId)
        {
            var result = await _availableCoursesService.DeleteAvailableCourse(availableCoursesId);
            if (result==null)
            {
                return NotFound();
            }
            return Ok(result);
        }


    }
}