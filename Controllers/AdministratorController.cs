using DiplomaThesisDigitalization.Services;
using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaThesisDigitalization.Controllers
{
    // Përcakto rrugën dhe vendos që kontrolleri të sillet si një API kontroller
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
        // Injektim i varësisë (Dependency) për shërbimin e Administratorit
        private readonly IAdministratorService _administratorService;
        public AdministratorController(IAdministratorService administratorService)
        {
            _administratorService = administratorService;
        }

        /*
         var jwt = Request.Cookies["jwt"];
            if (jwt == null)
            {
                return BadRequest("No logged user");
            }
         */

        // Metodë HTTP GET për të marrë të gjitha aplikimet e temave
        [HttpGet("all")]
        public async Task<IActionResult> GetAllThesis()
        {
            try
            {
                // Merr të gjitha temat duke përdorur shërbimin e administratorit dhe kthen si përgjigje HTTP OK
                var theses = await _administratorService.GetAllThesis();
                return Ok(theses);
            }
            catch (Exception ex)
            {
                // Kthe përgjigje HTTP BadRequest në rast të ndonjë përjashtimi
                return BadRequest($"Error getting all thesis applications: {ex.Message}");
            }
        }
        
        // Metodë HTTP PUT për të miratuar një aplikim specifik për diplomë
        [HttpPut("approve/{thesisApplicationId}")]
        public async Task<IActionResult> ApproveDiplomaThesisApplication(int thesisApplicationId)
        {
            // Kontrollo nëse përdoruesi është i loguar duke përdorur JWT token
            var jwt = Request.Cookies["jwt"];
            if (jwt == null)
            {
                // Kthe përgjigje HTTP BadRequest nëse nuk është i loguar
                return BadRequest("No logged user");
            }

            // Aprovo aplikimin e temës së diplomës dhe kthe përgjigje HTTP OK
            await _administratorService.ApproveDiplomaThesisApplication(jwt, thesisApplicationId);
            return Ok();
        }

        // Metodë HTTP PUT për të shënuar një temë diplomimi si të dorëzuar
        [HttpPut("submitted/{thesisApplicationId}")]
        public async Task<IActionResult> DiplomaThesisSubmitted(int thesisApplicationId)
        {
            // Kontrollo nëse përdoruesi është i loguar duke përdorur JWT token
            var jwt = Request.Cookies["jwt"];
            if (jwt == null)
            {
                // Kthe përgjigje HTTP BadRequest nëse nuk është i loguar
                return BadRequest("No logged user");
            }
            // Shëno temën e diplomës si të dorëzuar dhe kthe përgjigje HTTP OK
            await _administratorService.DiplomaThesisSubmitted(jwt, thesisApplicationId);
            return Ok();
        }
    
         // Metodë HTTP DELETE për të hequr një aplikim specifik të temës së diplomës
        [HttpDelete("remove/{thesisApplicationId}")]
        public async Task<IActionResult> RemoveDiplomaThesisApplication(int thesisApplicationId)
        {
            // Kontrollo nëse përdoruesi është i loguar duke përdorur JWT token
            var jwt = Request.Cookies["jwt"];
            if (jwt == null)
            {
                // Kthe përgjigje HTTP BadRequest nëse nuk është i loguar
                return BadRequest("No logged user");
            }
            // Heq aplikimin e temës së diplomës dhe kthe përgjigje HTTP OK
            await _administratorService.RemoveDiplomaThesisApplication(jwt, thesisApplicationId);
            return Ok();
        }
        
        // Metodë HTTP DELETE për të caktuar datën e afatit për një aplikim teme
        [HttpDelete("setDate")]
        public async Task<IActionResult> SetThesisDueDate(int thesisApplicationId, DateTime date)
        {
            // Kontrollo nëse përdoruesi është i loguar duke përdorur JWT token
            var jwt = Request.Cookies["jwt"];
            if (jwt == null)
            {    
                // Kthe përgjigje HTTP BadRequest nëse nuk është i loguar
                return BadRequest("No logged user");
            }
            // Cakto datën e afatit për aplikimin e temës dhe kthe përgjigje HTTP OK
            await _administratorService.SetThesisDueDate(jwt, thesisApplicationId, date);
            return Ok();
        }
    }
}
