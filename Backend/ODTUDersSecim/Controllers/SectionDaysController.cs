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
    public class SectionDaysController : Controller
    {
        private readonly SectionDaysService _subjectSectionDaysService;

        public SectionDaysController(SectionDaysService subjectsSectionDaysService)
        {
            _subjectSectionDaysService = subjectsSectionDaysService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(SectionDays), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SectionDays), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(List<SectionDays>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllSubjectSectionDays()
        {
            var subjectSections = await _subjectSectionDaysService.GetAllSubjectSectionDays();
            return Ok(subjectSections);
        }

        [HttpGet("{sectionCode}")]
        [ProducesResponseType(typeof(SectionDays), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SectionDays), (int)HttpStatusCode.NotFound)]

        public async Task<ActionResult> GetSubjectsSectionDays(int sectionCode)
        {

            var subjectSections = await _subjectSectionDaysService.GetSubjectsSectionDays(sectionCode);

            if (subjectSections == null) return NotFound();

            return Ok(subjectSections);
        }


        [HttpGet("{subjectSectionDaysId}")]
        [ProducesResponseType(typeof(SectionDays), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SectionDays), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetSubjectSectionDay(int subjectSectionDaysId)
        {
            var subjectSection = await _subjectSectionDaysService.GetSubjectSectionDay(subjectSectionDaysId);
            if (subjectSection == null)
            {
                return NotFound();
            }
            return Ok(subjectSection);
        }


        [HttpPost("listOfSubjects")]
        [ProducesResponseType(typeof(List<Subject>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetSchedule([FromBody] List<Subject> scheduleRequest)
        {
            var subjectSection = await _subjectSectionDaysService.GetSchedule(scheduleRequest);
            if (subjectSection == null)
            {
                return NotFound();
            }
            return Ok(subjectSection);
        }


        [HttpGet("{subjectCode}/{sectionId}")]
        [ProducesResponseType(typeof(SectionDaysDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SectionDaysDTO), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetSubjectSectionDetails(int subjectCode, int sectionId)
        {
            var subjectSections = await _subjectSectionDaysService.GetSubjectSectionDetails(subjectCode, sectionId);

            if (subjectSections == null) return NotFound();

            return Ok(subjectSections);
        }


        [HttpPost]
        [ProducesResponseType(typeof(SectionDays), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SectionDays), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> AddSubjectSection(SectionDaysDTO subjectSectionDaysDTO)
        {
            var addedSubjectSection = await _subjectSectionDaysService.AddSubjectSectionDays(subjectSectionDaysDTO);
            return Ok(addedSubjectSection);
        }

        [HttpPut]
        [ProducesResponseType(typeof(SectionDays), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SectionDays), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateSubjectSection(SectionDays subjectSectionDays)
        {
            var updatedSubjectSection = await _subjectSectionDaysService.UpdateSubjectSectionDays(subjectSectionDays);
            if (updatedSubjectSection == null)
            {
                return NotFound();
            }
            return Ok(updatedSubjectSection);
        }

        [HttpDelete("{subjectSectionId}")]
        [ProducesResponseType(typeof(SectionDays), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SectionDays), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteSubjectSection(int subjectSectionId)
        {
            var deletedSubjectSection = await _subjectSectionDaysService.DeleteSubjectSectionDays(subjectSectionId);
            if (deletedSubjectSection == null)
            {
                return NotFound();
            }
            return Ok(deletedSubjectSection);
        }
    }
}

