using DiplomaThesisDigitalization.Services;
using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("submitapplication")]
        public async Task<IActionResult> SubmitThesisApplication(string titleName, int mentorId)
        {
            var jwt = Request.Cookies["jwt"];
            if (jwt == null)
            {
                return BadRequest("No logged user");
            }

            try
            {
                await _studentService.SubmitThesisApplication(jwt, titleName, mentorId);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }


        [HttpDelete("cancelapplication")]
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
