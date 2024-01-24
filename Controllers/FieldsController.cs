using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaThesisDigitalization.Controllers
{
    // Atributi Route për të përcaktuar rrugën e API, dhe atributi ApiController për të shënuar këtë si një API kontroller
    [Route("api/[controller]")]
    [ApiController]
    public class FieldsController : ControllerBase
    {
        // Fusha (Field) private për shërbimin e Fushës (Field), përdoret për injektim varësie (Dependency)
        private readonly IFieldService _fieldService;

        // Konstruktori për të inicializuar shërbimin e Fushës (Field) përmes injektimit të varësisë (Dependency)
        public FieldsController(IFieldService fieldService)
        {
            _fieldService = fieldService;
        }

        // Metoda HTTP POST për të krijuar një fushë (field) të re
        [HttpPost]
        public async Task<IActionResult> CreateField(string fieldName, int departmentId)
        {
            try
            {
                // Përpiqet të krijojë një fushë (field) dhe kthen përgjigje HTTP OK nëse është e suksesshme
                await _fieldService.CreateField(fieldName, departmentId);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                // Kthen një përgjigje HTTP Unauthorized nëse ndodh një përjashtim i paautorizuar
                return Unauthorized(ex.Message);
            }
        }

        // Metoda HTTP GET për të marrë të gjitha fushat (fields)
        [HttpGet("all")]
        public async Task<IActionResult> GetAllFields()
        {
            try
            {
                // Përpiqet të marrë të gjitha fushat (fields) dhe i kthen me përgjigje HTTP OK
                var fields = await _fieldService.GetAllFields();
                return Ok(fields);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Kthen një përgjigje HTTP Unauthorized nëse ndodh një përjashtim i paautorizuar
                return Unauthorized(ex.Message);
            }
        }

        // Metoda HTTP DELETE për të fshirë një fushë (field) sipas emrit të saj
        [HttpDelete("{fieldName}")]
        public async Task<IActionResult> DeleteField(string fieldName)
        {
            try
            {
                // Përpiqet të fshijë fushën (field) e specifikuar dhe kthen përgjigje HTTP OK nëse është e suksesshme
                await _fieldService.DeleteField(fieldName);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                // Kthen një përgjigje HTTP Unauthorized nëse ndodh një përjashtim i paautorizuar
                return Unauthorized(ex.Message);
            }
        }

        // Metoda HTTP GET për të marrë mentorët e lidhur me një fushë (field) specifike
        [HttpGet("{fieldName}/mentors")]
        public async Task<IActionResult> GetMentorsFromField(string fieldName)
        {
            try
            {
                // Përpiqet të marrë mentorët nga një fushë (field) specifike dhe i kthen me përgjigje HTTP OK
                var fields = await _fieldService.GetMentorsFromField(fieldName);
                return Ok(fields);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Kthen një përgjigje HTTP Unauthorized nëse ndodh një përjashtim i paautorizuar
                return Unauthorized(ex.Message);
            }
        }

        // Metoda HTTP GET për të marrë studentët e lidhur me një fushë (field) specifike
        [HttpGet("{fieldName}/students")]
        public async Task<IActionResult> GetStudentsFromField(string fieldName)
        {
            try
            {
                // Përpiqet të marrë studentët nga një fushë (field) specifike dhe i kthen me përgjigje HTTP OK
                var fields = await _fieldService.GetStudentsFromField(fieldName);
                return Ok(fields);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Kthen një përgjigje HTTP Unauthorized nëse ndodh një përjashtim i paautorizuar
                return Unauthorized(ex.Message);
            }
        }
    }
}
