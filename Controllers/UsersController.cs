using DiplomaThesisDigitalization.Models.DTOs;
using DiplomaThesisDigitalization.Models.Entities;
using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaThesisDigitalization.Controllers
{
    // Atributi Route për të përcaktuar rrugën e API, dhe atributi ApiController për të shënuar këtë si një kontroller API
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // Fusha (Field) private për shërbimin e përdoruesit, përdoret për injektim varësie (dependency)
        private readonly IUserService _userService;

        // Konstruktori për të inicializuar shërbimin e përdoruesit përmes injektimit të varësisë (dependency)
        public UsersController(IUserService userService) 
        {
            _userService = userService;
        }

        // Metoda HTTP POST për të krijuar një administrator të ri
        [HttpPost("administrator")]
        public async Task<ActionResult<User>> CreateAdmin([FromBody] CreateAdminDTO adminDTO)
        {
            // Shton një administrator të ri duke përdorur DTO-në e dhënë dhe kthen përgjigje HTTP OK
            await _userService.AddAdmin(adminDTO);
            return Ok();
        }

        // Metoda HTTP DELETE për të fshirë një administrator sipas ID-së
        [HttpDelete("administrator")]
        public async Task<ActionResult<User>> DeleteAdmin(int adminId)
        {
            // Fshin administratorin me ID të specifikuar dhe kthen përgjigje HTTP OK
            await _userService.DeleteAdmin(adminId);
            return Ok();
        }

        // Metoda HTTP POST për të krijuar një student të ri
        [HttpPost("student")]
        public async Task<ActionResult<User>> CreateStudent([FromBody] CreateStudentDTO studentDTO)
        {
            // Shton një student të ri duke përdorur DTO-në e dhënë dhe kthen përgjigje HTTP OK
            await _userService.AddStudent(studentDTO);
            return Ok();
        }

        // Metoda HTTP GET për të marrë të gjithë studentët
        [HttpGet("students")]
        public async Task<ActionResult> GetStudents()
            {
                var students = await _userService.GetAllStudentsAsync();
                return Ok(students);
            }

        // Metoda HTTP DELETE për të fshirë një student sipas ID-së
        [HttpDelete("student")]
        public async Task<ActionResult<User>> DeleteStudent(int studentId)
        {
            await _userService.DeleteStudent(studentId);
            return Ok();
        }

        // Metoda HTTP POST për të krijuar një mentor të ri
        [HttpPost("mentor")]
        public async Task<ActionResult<User>> CreateMentor([FromBody] CreateMentorDTO mentorDTO)
        {
            // Shton një mentor të ri duke përdorur DTO-në e dhënë dhe kthen përgjigje HTTP OK
            await _userService.AddMentor(mentorDTO);
            return Ok();
        }

        // Metoda HTTP GET për të marrë të gjithë mentorët
        [HttpGet("mentors")]
        public async Task<ActionResult> GetMentors()
            {
                // Merr të gjithë mentorët dhe i kthen ato me përgjigje HTTP OK
                var mentors = await _userService.GetAllMentorsAsync();
                return Ok(mentors);
            }

        // Metoda HTTP DELETE për të fshirë një mentor sipas ID-së
        [HttpDelete("mentor")]
        public async Task<ActionResult<User>> DeleteMentor(int mentorId)
        {
            // Fshin mentorin me ID të specifikuar dhe kthen përgjigje HTTP OK
            await _userService.DeleteMentor(mentorId);
            return Ok();
        }
    }
}
