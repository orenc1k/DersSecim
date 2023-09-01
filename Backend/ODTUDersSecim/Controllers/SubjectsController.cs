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
    public class SubjectsController : Controller
    {
        private readonly SubjectsService _subjectsService;

        public SubjectsController(SubjectsService subjectsService)
        {
            _subjectsService = subjectsService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Subjects), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Subjects), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(List<Subjects>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSubjects()
        {
            var subjects = await _subjectsService.GetSubjects();
            return Ok(subjects);
        }

        [HttpGet("{subjectCode}")]
        [ProducesResponseType(typeof(Subjects), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Subjects), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetSubject(int subjectCode)
        {
            var subject = await _subjectsService.GetSubject(subjectCode);
            if (subject == null)
            {
                return NotFound();
            }
            return Ok(subject);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Subjects), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Subjects), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> AddSubject(Subjects subject)
        {
            var addedSubject = await _subjectsService.AddSubject(subject);
            return Ok(addedSubject);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Subjects), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Subjects), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateSubject(Subjects subject)
        {
            var updatedSubject = await _subjectsService.UpdateSubject(subject);
            if (updatedSubject == null)
            {
                return NotFound();
            }
            return Ok(updatedSubject);
        }

        [HttpDelete("{subjectCode}")]
        [ProducesResponseType(typeof(Subjects), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Subjects), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteSubject(int subjectCode)
        {
            var deletedSubject = await _subjectsService.DeleteSubject(subjectCode);
            if (deletedSubject == null)
            {
                return NotFound();
            }
            return Ok(deletedSubject);
        }
    }
}

