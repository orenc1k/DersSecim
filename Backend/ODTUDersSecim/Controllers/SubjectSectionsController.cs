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
    public class SubjectSectionsController : Controller
    {
        private readonly SubjectSectionsService _subjectSectionsService;

        public SubjectSectionsController(SubjectSectionsService subjectsSectionService)
        {
            _subjectSectionsService = subjectsSectionService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(SubjectSections), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SubjectSections), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(List<SubjectSections>), (int)HttpStatusCode.OK)]
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

        [HttpPost]
        [ProducesResponseType(typeof(SubjectSections), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SubjectSections), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> AddSubjectSection(SubjectSections subjectSection)
        {
            var addedSubjectSection = await _subjectSectionsService.AddSubjectSection(subjectSection);
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

