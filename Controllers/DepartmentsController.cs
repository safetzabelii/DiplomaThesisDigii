using DiplomaThesisDigitalization.Models.DTOs;
using DiplomaThesisDigitalization.Services;
using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaThesisDigitalization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("departments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _departmentService.GetAllDepartments();
            return Ok(departments);
        }

        [HttpGet("departments/{facultyId}")]
        public async Task<IActionResult> GetDepartmentsByFaculty(int facultyId)
        {
            var departments = await _departmentService.GetDepartmentsByFaculty(facultyId);
            return Ok(departments);
        }

        [HttpPost("department")]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentDTO departmentDTO)
        {
                await _departmentService.CreateDepartment(departmentDTO);
                return Ok();
        }

        [HttpDelete("department")]
        public async Task<IActionResult> DeleteDepartment(int departmentId)
        {
            await _departmentService.DeleteDepartment(departmentId);
            return Ok();
        }
    }
}
