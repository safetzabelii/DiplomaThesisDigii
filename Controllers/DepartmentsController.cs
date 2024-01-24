using DiplomaThesisDigitalization.Models.DTOs;
using DiplomaThesisDigitalization.Services;
using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaThesisDigitalization.Controllers
{
    // Përcakto rrugën dhe vendos që kontrolleri të sillet si një API kontroller
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        // Injektim i varësisë (Dependency) për shërbimin e Departamentit
        private readonly IDepartmentService _departmentService;

        // Konstruktori për të inicializuar shërbimin e Departamentit
        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // Metodë HTTP GET për të marrë të gjitha departamentet
        [HttpGet("departments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            // Merr të gjitha departamentet duke përdorur shërbimin e departamentit dhe kthe si përgjigje HTTP OK
            var departments = await _departmentService.GetAllDepartments();
            return Ok(departments);
        }

        // Metodë HTTP GET për të marrë departamentet nga një fakultet specifik
        [HttpGet("departments/{facultyId}")]
        public async Task<IActionResult> GetDepartmentsByFaculty(int facultyId)
        {
            // Merr departamentet për një fakultet specifik duke përdorur shërbimin e departamentit dhe kthe si përgjigje HTTP OK
            var departments = await _departmentService.GetDepartmentsByFaculty(facultyId);
            return Ok(departments);
        }

        // Metodë HTTP POST për të krijuar një departament të ri
        [HttpPost("department")]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentDTO departmentDTO)
        {
                // Krijo një departament të ri duke përdorur shërbimin e departamentit dhe kthe përgjigje HTTP OK
                await _departmentService.CreateDepartment(departmentDTO);
                return Ok();
        }

        // Metodë HTTP DELETE për të fshirë një departament sipas ID-së së tij
        [HttpDelete("department")]
        public async Task<IActionResult> DeleteDepartment(int departmentId)
        {
            // Fshi një departament duke përdorur shërbimin e departamentit dhe kthe përgjigje HTTP OK
            await _departmentService.DeleteDepartment(departmentId);
            return Ok();
        }
    }
}
