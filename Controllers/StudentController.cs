using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using DiplomaThesisDigitalization.Models.DTOs;

namespace DiplomaThesisDigitalization.Controllers
{
    // Atributi Route për të përcaktuar rrugën e API, dhe atributi ApiController për të shënuar këtë si një API kontroller
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
    
        // Fusha (Field) private për shërbimin e Studentit, përdoret për injektim varësie (dependency)
        private readonly IStudentService _studentService;

        // Konstruktori për të inicializuar shërbimin e Studentit përmes injektimit të varësisë (dependency)
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        
        // Metoda HTTP GET për të marrë temën aktuale të studentit të loguar
        [HttpGet("current-thesis")]
        public async Task<IActionResult> GetCurrentThesis()
        {
            // Kontrollon nëse përdoruesi është i loguar duke përdorur JWT token
            var jwt = Request.Cookies["jwt"];
            if (jwt == null)
            {    
                // Kthen përgjigje HTTP BadRequest nëse nuk ka përdorues të loguar
                return BadRequest("No logged user");
            }

            try
            {
                // Merr detajet e temës aktuale për studentin dhe i kthen nëse janë gjetur
                var currentThesis = await _studentService.GetCurrentThesis(jwt);
                if (currentThesis != null)
                {
                    return Ok(currentThesis);
                }
                else
                {
                    // Kthen përgjigje HTTP NotFound nëse nuk gjendet asnjë temë aktuale
                    return NotFound("No current thesis");
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                // Kthen një përgjigje HTTP Unauthorized nëse ndodh një përjashtim i paautorizuar
                return Unauthorized(ex.Message);
            }
        }

        // Metoda HTTP GET për të marrë ID-në e temës aktuale të studentit të loguar
        [HttpGet("current-thesis-id")]
        public async Task<IActionResult> GetCurrentThesisId()
        {
            // Kontrollon nëse përdoruesi është i loguar duke përdorur JWT token
            var jwt = Request.Cookies["jwt"];
            if (jwt == null)
            {
                // Kthen përgjigje HTTP BadRequest nëse nuk ka përdorues të loguar
                return BadRequest("No logged user");
            }

            try
            {
                // Merr ID e temës aktuale për studentin dhe e kthen nëse është gjetur
                var thesisId = await _studentService.GetCurrentThesisId(jwt);
                if (thesisId != null)
                {
                    return Ok(thesisId);
                }
                else
                {
                    // Kthen përgjigje HTTP NotFound nëse nuk gjendet ID e temës aktuale
                    return NotFound("No current thesis ID");
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                // Kthen një përgjigje HTTP Unauthorized nëse ndodh një përjashtim i paautorizuar
                return Unauthorized(ex.Message);
            }
        }


        
        // Metoda HTTP POST për studentët të paraqesin një aplikim për temë
        [HttpPost("submitapplication")]
        public async Task<IActionResult> SubmitThesisApplication([FromBody] CreateApplicationDTO applicationDTO)
        {
            // Kontrollon nëse përdoruesi është i loguar duke përdorur JWT token
            var jwt = Request.Cookies["jwt"];
            if (jwt == null)
            {
                // Kthen përgjigje HTTP BadRequest nëse nuk ka përdorues të loguar
                return BadRequest("No logged user");
            }


            try
            {
                // Paraqet aplikimin për temë dhe kthen përgjigje HTTP OK nëse është e suksesshme
                await _studentService.SubmitThesisApplication(jwt, applicationDTO.titleName, applicationDTO.mentorId);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                // Kthen një përgjigje HTTP Unauthorized nëse ndodh një përjashtim i paautorizuar
                return Unauthorized(ex.Message);
            }
        }

        // Metoda HTTP DELETE për studentët të anulojnë një aplikim për temë
        [HttpDelete("cancelapplication/{thesisApplicationId}")]
        public async Task<IActionResult> CancelThesisApplication(int thesisApplicationId)
        {
            // Kontrollon nëse përdoruesi është i loguar duke përdorur JWT token
            var jwt = Request.Cookies["jwt"];
            if (jwt == null)
            {
                // Kthen përgjigje HTTP BadRequest nëse nuk ka përdorues të loguar
                return BadRequest("No logged user");
            }


            try
            {
                // Anulon aplikimin e specifikuar për temë dhe kthen përgjigje HTTP OK nëse është e suksesshme
                await _studentService.CancelThesisApplication(jwt, thesisApplicationId);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                // Kthen një përgjigje HTTP Unauthorized nëse ndodh një përjashtim i paautorizuar
                return Unauthorized(ex.Message);
            }
        }
        
    }
}
