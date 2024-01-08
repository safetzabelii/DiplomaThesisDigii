using DiplomaThesisDigitalization.Services;
using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaThesisDigitalization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MentorController : ControllerBase
    {
        private readonly IMentorService _mentorService;

        public MentorController(IMentorService mentorService)
        {
            _mentorService = mentorService;
        }

        [HttpPut("departments/{departmentId}")]
        public async Task<IActionResult> AddDepartment(int departmentId)
        {
            var jwt = Request.Cookies["jwt"];
            if (jwt == null)
            {
                return BadRequest("No logged user");
            }

            try
            {
                await _mentorService.AddDepartment(jwt, departmentId);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpDelete("departments/{departmentId}")]
        public async Task<IActionResult> RemoveDepartment(int departmentId)
        {
            var jwt = Request.Cookies["jwt"];
            if (jwt == null)
            {
                return BadRequest("No logged user");
            }

            try
            {
                await _mentorService.RemoveDepartment(jwt, departmentId);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPut("fields/{fieldId}")]
        public async Task<IActionResult> AddField(int fieldId)
        {
            var jwt = Request.Cookies["jwt"];
            if (jwt == null)
            {
                return BadRequest("No logged user");
            }

            try
            {
                await _mentorService.AddField(jwt, fieldId);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpDelete("fields/{fieldId}")]
        public async Task<IActionResult> RemoveField(int fieldId)
        {
            var jwt = Request.Cookies["jwt"];
            if (jwt == null)
            {
                return BadRequest("No logged user");
            }

            try
            {
                await _mentorService.RemoveField(jwt, fieldId);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPut("assessDiplomaThesis")]
        public async Task<IActionResult> AssessDiplomaThesis(int diplomaThesisId, byte assessment)
        {
            var jwt = Request.Cookies["jwt"];
            if (jwt == null)
            {
                return BadRequest("No logged user");
            }

            try
            {
                await _mentorService.AssessDiplomaThesis(jwt, diplomaThesisId, assessment);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPut("availability")]
        public async Task<IActionResult> SetAvailability(string availability)
        {
            var jwt = Request.Cookies["jwt"];
            if (jwt == null)
            {
                return BadRequest("No logged user");
            }

            try
            {
                await _mentorService.SetAvailability(jwt, availability);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
