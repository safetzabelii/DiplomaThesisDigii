using DiplomaThesisDigitalization.Models.DTOs;
using DiplomaThesisDigitalization.Models.Entities;
using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaThesisDigitalization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpPost("administrator")]
        public async Task<ActionResult<User>> CreateAdmin([FromBody] CreateAdminDTO adminDTO)
        {
            await _userService.AddAdmin(adminDTO);
            return Ok();
        }

        [HttpDelete("administrator")]
        public async Task<ActionResult<User>> DeleteAdmin(int adminId)
        {
            await _userService.DeleteAdmin(adminId);
            return Ok();
        }
        [HttpPost("student")]
        public async Task<ActionResult<User>> CreateStudent([FromBody] CreateStudentDTO studentDTO)
        {
            await _userService.AddStudent(studentDTO);
            return Ok();
        }

        [HttpGet("students")]
        public async Task<ActionResult> GetStudents()
            {
                var students = await _userService.GetAllStudentsAsync();
                return Ok(students);
            }


        [HttpDelete("student")]
        public async Task<ActionResult<User>> DeleteStudent(int studentId)
        {
            await _userService.DeleteStudent(studentId);
            return Ok();
        }
        [HttpPost("mentor")]
        public async Task<ActionResult<User>> CreateMentor([FromBody] CreateMentorDTO mentorDTO)
        {
            await _userService.AddMentor(mentorDTO);
            return Ok();
        }
         [HttpGet("mentors")]
        public async Task<ActionResult> GetMentors()
            {
                var mentors = await _userService.GetAllMentorsAsync();
                return Ok(mentors);
            }


        [HttpDelete("mentor")]
        public async Task<ActionResult<User>> DeleteMentor(int mentorId)
        {
            await _userService.DeleteMentor(mentorId);
            return Ok();
        }
    }
}
