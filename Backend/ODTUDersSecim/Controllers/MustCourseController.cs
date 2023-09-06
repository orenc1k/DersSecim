using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ODTUDersSecim.Services;
using ODTUDersSecim.DTOs;
using Microsoft.AspNetCore.Mvc;
using ODTUDersSecim.Models;
using System.Net;

namespace ODTUDersSecim.Controllers
{
    [Route("api/[controller]/[action]/")]
    [ApiController]
    public class MustCourseController : Controller
    {
        private readonly MustCourseService _mustCourseService;

        public MustCourseController(MustCourseService mustCourseService)
        {
            _mustCourseService = mustCourseService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(MustCourseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(MustCourseDTO), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(List<MustCourseDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllMustCourses()
        {
            var subjects = await _mustCourseService.GetAllMustCourses();
            return Ok(subjects);
        }

        [HttpGet("{mustCourseId}")]
        [ProducesResponseType(typeof(MustCourses), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(MustCourses), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetMustCourse(int mustCourseId)
        {
            var subject = await _mustCourseService.GetMustCourse(mustCourseId);
            if (subject == null)
            {
                return NotFound();
            }
            return Ok(subject);
        }

        [HttpGet("{deptCode}/{semester}")] 
        [ProducesResponseType(typeof(MustCourseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(MustCourseDTO), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetSemesterMustCourses(int deptCode, int semester)
        {
            var subject = await _mustCourseService.GetSemesterMustCourses(semester,deptCode);
            if (subject == null)
            {
                return NotFound();
            }
            return Ok(subject);
        }
        [HttpPost]
        [ProducesResponseType(typeof(MustCourseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(MustCourseDTO), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> AddMustCourse(MustCourseDTO mustCourseDTO)
        {
            var addedSubject = await _mustCourseService.AddMustCourse(mustCourseDTO);
            return Ok(addedSubject);
        }

        [HttpPut]
        [ProducesResponseType(typeof(MustCourses), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(MustCourses), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateMustCourse(MustCourses mustCourse)
        {
            var updatedSubject = await _mustCourseService.UpdateMustCourse(mustCourse);
            if (updatedSubject == null)
            {
                return NotFound();
            }
            return Ok(updatedSubject);
        }

        [HttpDelete("{mustCourseId}")]
        [ProducesResponseType(typeof(MustCourses), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(MustCourses), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteDepartment(int mustCourseId)
        {
            var deletedSubject = await _mustCourseService.DeleteMustCourse(mustCourseId);
            if (deletedSubject == null)
            {
                return NotFound();
            }
            return Ok(deletedSubject);
        }
    }
}

