using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ODTUDersSecim.Services;
using ODTUDersSecim.DTOs;
using Microsoft.AspNetCore.Mvc;
using ODTUDersSecim.Models;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace ODTUDersSecim.Controllers
{
    [Route("api/[controller]/[action]/")]
    [ApiController]
    public class SubjectSectionsController : Controller
    {
        private readonly SubjectSectionsService _subjectSectionsService;
        private readonly ODTUDersSecimDBContext _oDTUDersSecimDBContext;

        public SubjectSectionsController(SubjectSectionsService subjectsSectionService, ODTUDersSecimDBContext oDTUDersSecimDBContext)
        {
            _subjectSectionsService = subjectsSectionService;
            _oDTUDersSecimDBContext = oDTUDersSecimDBContext;
        }

        [HttpGet]
        [ProducesResponseType(typeof(SubjectSectionsDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SubjectSectionsDTO), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(List<SubjectSectionsDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllSubjectSections()
        {
            var subjectSections = await _subjectSectionsService.GetAllSubjectSections();
            return Ok(subjectSections);
        }

        [HttpGet("{subjectCode}")]
        [ProducesResponseType(typeof(SubjectSections),(int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SubjectSections), (int)HttpStatusCode.NotFound)]

        public async Task<ActionResult> GetSubjectSections(int subjectCode)
        {

            var subjectSections = await _subjectSectionsService.GetSubjectSections(subjectCode);

            if (subjectSections == null) return NotFound();

            return Ok(subjectSections);
        }


        [HttpGet("{subjectSectionId}")]
        [ProducesResponseType(typeof(SubjectSections), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SubjectSections), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetSubjectSection(int subjectSectionId)
        {
            var subjectSection = await _subjectSectionsService.GetSubjectSection(subjectSectionId);
            if (subjectSection == null)
            {
                return NotFound();
            }
            return Ok(subjectSection);
        }


        [HttpGet("{subjectCode}")]
        public async Task<ActionResult<List<SectionDays>>> GetSectionDays(int subjectCode, float? cumGPA, string? surname, string? courseGrade)
        {
            try
            {
               var matchingDays= await _subjectSectionsService.GetSectionDays(subjectCode, cumGPA, surname, courseGrade);

                return Ok(matchingDays);
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                Console.WriteLine("Error: " + ex.Message);
                return BadRequest("An error occurred while fetching section days.");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(SubjectSections), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SubjectSections), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> AddSubjectSection(SubjectSectionsDTO subjectSectionDTO)
        {
            var addedSubjectSection = await _subjectSectionsService.AddSubjectSection(subjectSectionDTO);
            return Ok(addedSubjectSection);
        }

        [HttpPut]
        [ProducesResponseType(typeof(SubjectSections), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SubjectSections), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateSubjectSection(SubjectSections subjectSection)
        {
            var updatedSubjectSection = await _subjectSectionsService.UpdateSubjectSection(subjectSection);
            if (updatedSubjectSection == null)
            {
                return NotFound();
            }
            return Ok(updatedSubjectSection);
        }

        [HttpDelete("{subjectSectionId}")]
        [ProducesResponseType(typeof(SubjectSections), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SubjectSections), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteSubjectSection(int subjectSectionId)
        {
            var deletedSubjectSection = await _subjectSectionsService.DeleteSubjectSection(subjectSectionId);
            if (deletedSubjectSection == null)
            {
                return NotFound();
            }
            return Ok(deletedSubjectSection);
        }
    }
}

