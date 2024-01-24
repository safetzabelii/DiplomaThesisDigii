using DiplomaThesisDigitalization.Services;
using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaThesisDigitalization.Controllers
{
    // Përcakto rrugën dhe vendos që kontrolleri të sillet si një API kontroller
    [Route("api/[controller]")]
    [ApiController]
    public class FacultiesController : ControllerBase
    {
        // Injektim i varësisë (Dependency) për shërbimin e Fakultetit
        private readonly IFacultyService _facultyService;

        // Konstruktori për të inicializuar shërbimin e Fakultetit
        public FacultiesController(IFacultyService facultyService)
        {
            _facultyService = facultyService;
        }

        // Metodë HTTP POST për të krijuar një fakultet të ri
        [HttpPost("faculty")]
        public async Task<IActionResult> CreateFaculty(string facultyName)
        {
                // Krijo një fakultet të ri duke përdorur shërbimin e fakultetit dhe kthe përgjigje HTTP OK
                await _facultyService.CreateFaculty(facultyName);
                return Ok();
        }

        // Metodë HTTP GET për të marrë të gjitha fakultetet
        [HttpGet("faculties")]
        public async Task<IActionResult> GetAllFaculties()
        {
                // Merr të gjitha fakultetet duke përdorur shërbimin e fakultetit dhe kthe si përgjigje HTTP OK
                var faculties = await _facultyService.GetAllFaculties();
                return Ok(faculties);
        }

        // Metodë HTTP DELETE për të fshirë një fakultet sipas emrit të tij
        [HttpDelete("faculty")]
        public async Task<IActionResult> DeleteFaculty(string facultyName)
        {
                // Fshi një fakultet duke përdorur shërbimin e fakultetit dhe kthe përgjigje HTTP OK
                await _facultyService.DeleteFaculty(facultyName);
                return Ok();
        }
    }
}
