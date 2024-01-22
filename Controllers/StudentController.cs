using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using DiplomaThesisDigitalization.Models.DTOs;

namespace DiplomaThesisDigitalization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
    
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        
        [HttpGet("current-thesis")]
        public async Task<IActionResult> GetCurrentThesis()
        {
            var jwt = Request.Cookies["jwt"];
            if (jwt == null)
            {
                return BadRequest("No logged user");
            }

            try
            {
                var currentThesis = await _studentService.GetCurrentThesis(jwt);
                
                if (currentThesis != null)
                {
                    return Ok(currentThesis);
                }
                else
                {
                    return NotFound("No current thesis");
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        [HttpGet("current-thesis-id")]
        public async Task<IActionResult> GetCurrentThesisId()
        {
            var jwt = Request.Cookies["jwt"];
            if (jwt == null)
            {
                return BadRequest("No logged user");
            }

            try
            {
                var thesisId = await _studentService.GetCurrentThesisId(jwt);
                
                if (thesisId != null)
                {
                    return Ok(thesisId);
                }
                else
                {
                    return NotFound("No current thesis ID");
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }


        

        [HttpPost("submitapplication")]
        public async Task<IActionResult> SubmitThesisApplication([FromBody] CreateApplicationDTO applicationDTO)
        {
            var jwt = Request.Cookies["jwt"];
            if (jwt == null)
            {
                return BadRequest("No logged user");
            }


            try
            {
                await _studentService.SubmitThesisApplication(jwt, applicationDTO.titleName, applicationDTO.mentorId);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpDelete("cancelapplication/{thesisApplicationId}")]
        public async Task<IActionResult> CancelThesisApplication(int thesisApplicationId)
        {
            var jwt = Request.Cookies["jwt"];
            if (jwt == null)
            {
                return BadRequest("No logged user");
            }


            try
            {
                await _studentService.CancelThesisApplication(jwt, thesisApplicationId);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        
    }
}
