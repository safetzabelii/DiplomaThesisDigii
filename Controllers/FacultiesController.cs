using DiplomaThesisDigitalization.Models.DTOs;
using DiplomaThesisDigitalization.Models.Entities;
using DiplomaThesisDigitalization.Services;
using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaThesisDigitalization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultiesController : ControllerBase
    {
        private readonly IFacultyService _facultyService;

        public FacultiesController(IFacultyService facultyService)
        {
            _facultyService = facultyService;
        }

       [HttpPost("faculty")]
        public async Task<ActionResult> CreateFaculty([FromBody] CreateFacultyDTO facultyDTO)
        {
            if (facultyDTO == null || string.IsNullOrWhiteSpace(facultyDTO.facultyName))
            {
                return BadRequest("Invalid faculty data");
            }

            await _facultyService.CreateFaculty(facultyDTO.facultyName);
            return Ok();
        }


        [HttpGet("faculties")]
        public async Task<IActionResult> GetAllFaculties()
        {
                var faculties = await _facultyService.GetAllFaculties();
                return Ok(faculties);
        }

        [HttpDelete("faculty")]
        public async Task<IActionResult> DeleteFaculty([FromBody] string facultyName)
        {
            await _facultyService.DeleteFaculty(facultyName);
            return Ok();
        }

    }
}
