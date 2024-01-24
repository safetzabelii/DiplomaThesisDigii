using DiplomaThesisDigitalization.Services;
using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaThesisDigitalization.Controllers
{
    // Atributi Route për të përcaktuar rrugën e API, dhe atributi ApiController për të shënuar këtë si një API kontroller
    [Route("api/[controller]")]
    [ApiController]
    public class MentorController : ControllerBase
    {
        // Fusha (Field) private për shërbimin e Mentorit, përdoret për injektim varësie (dependency)
        private readonly IMentorService _mentorService;

        // Konstruktori për të inicializuar shërbimin e Mentorit përmes injektimit të varësisë (dependency)
        public MentorController(IMentorService mentorService)
        {
            _mentorService = mentorService;
        }

        // Metoda HTTP PUT për të shtuar një departament te mentori
        [HttpPut("departments/{departmentId}")]
        public async Task<IActionResult> AddDepartment(int departmentId)
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
                // Shton një departament te mentori dhe kthen përgjigje HTTP OK nëse është e suksesshme
                await _mentorService.AddDepartment(jwt, departmentId);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                // Kthen një përgjigje HTTP Unauthorized nëse ndodh një përjashtim i paautorizuar
                return Unauthorized(ex.Message);
            }
        }

        // Metoda HTTP DELETE për të hequr një departament nga mentori
        [HttpDelete("departments/{departmentId}")]
        public async Task<IActionResult> RemoveDepartment(int departmentId)
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
                // Heq një departament nga mentori dhe kthen përgjigje HTTP OK nëse është e suksesshme
                await _mentorService.RemoveDepartment(jwt, departmentId);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                // Kthen një përgjigje HTTP Unauthorized nëse ndodh një përjashtim i paautorizuar
                return Unauthorized(ex.Message);
            }
        }

        // Metoda HTTP PUT për të shtuar një fushë (field) te mentori
        [HttpPut("fields/{fieldId}")]
        public async Task<IActionResult> AddField(int fieldId)
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
                // Shton një fushë (field) te mentori dhe kthen përgjigje HTTP OK nëse është e suksesshme
                await _mentorService.AddField(jwt, fieldId);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                // Kthen një përgjigje HTTP Unauthorized nëse ndodh një përjashtim i paautorizuar
                return Unauthorized(ex.Message);
            }
        }

        // Metoda HTTP DELETE për të hequr një fushë (field) nga mentori
        [HttpDelete("fields/{fieldId}")]
        public async Task<IActionResult> RemoveField(int fieldId)
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
                // Heq një fushë (field) nga mentori dhe kthen përgjigje HTTP OK nëse është e suksesshme
                await _mentorService.RemoveField(jwt, fieldId);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                // Kthen një përgjigje HTTP Unauthorized nëse ndodh një përjashtim i paautorizuar
                return Unauthorized(ex.Message);
            }
        }

        // Metoda HTTP PUT për mentori të vlerësojë një temë diplome
        [HttpPut("assessDiplomaThesis")]
        public async Task<IActionResult> AssessDiplomaThesis(int diplomaThesisId, byte assessment)
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
                // Vlerëson temën e diplomës dhe kthen përgjigje HTTP OK nëse është e suksesshme
                await _mentorService.AssessDiplomaThesis(jwt, diplomaThesisId, assessment);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                // Kthen një përgjigje HTTP Unauthorized nëse ndodh një përjashtim i paautorizuar
                return Unauthorized(ex.Message);
            }
        }

        // Metoda HTTP PUT për të vendosur disponueshmërinë e mentorit
        [HttpPut("availability")]
        public async Task<IActionResult> SetAvailability(string availability)
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
                // Vendos disponueshmërinë e mentorit dhe kthen përgjigje HTTP OK nëse është e suksesshme
                await _mentorService.SetAvailability(jwt, availability);
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
