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
    public class CengController : Controller
    {
        private readonly CengDersSecimService _cengDersSecimService;

        public CengController(CengDersSecimService cengDersSecimService)
        {
            _cengDersSecimService = cengDersSecimService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CengSubjects), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CengSubjects), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(List<CengSubjects>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSubjects()
        {
            var subjects = await _cengDersSecimService.GetSubjects();
            return Ok(subjects);
        }

        [HttpGet("{subjectCode}")]
        [ProducesResponseType(typeof(CengSubjects), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CengSubjects), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetSubject(int subjectCode)
        {
            var subject = await _cengDersSecimService.GetSubject(subjectCode);
            if (subject == null)
            {
                return NotFound();
            }
            return Ok(subject);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CengSubjects), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CengSubjects), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> AddSubject( CengSubjects cengSubjects)
        {
            var addedSubject = await _cengDersSecimService.AddSubject(cengSubjects);
            return Ok(addedSubject);
        }

        [HttpPut]
        [ProducesResponseType(typeof(CengSubjects), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CengSubjects), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateSubject(CengSubjects subject)
        {
            var updatedSubject = await _cengDersSecimService.UpdateSubject(subject);
            if (updatedSubject == null)
            {
                return NotFound();
            }
            return Ok(updatedSubject);
        }

        [HttpDelete("{subjectCode}")]
        [ProducesResponseType(typeof(CengSubjects), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CengSubjects), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteSubject(int subjectCode)
        {
            var deletedSubject = await _cengDersSecimService.DeleteSubject(subjectCode);
            if (deletedSubject == null)
            {
                return NotFound();
            }
            return Ok(deletedSubject);
        }
    }
}

