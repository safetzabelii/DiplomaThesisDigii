using DiplomaThesisDigitalization.Services;
using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaThesisDigitalization.Controllers
{
    // Atributi Route për të përcaktuar rrugën e API, dhe atributi ApiController për të shënuar këtë si një API kontroller
    [Route("api/[controller]")]
    [ApiController]
    public class TitlesController : ControllerBase
    {
        // Fusha (Field) private për shërbimin e Titullit, përdoret për injektim varësie (dependency)
        private readonly ITitleService _titleService;

        // Konstruktori për të inicializuar shërbimin e Titullit përmes injektimit të varësisë (dependency)
        public TitlesController(ITitleService titleService)
        {
            _titleService = titleService;
        }
        
        // Metoda HTTP POST për të krijuar një titull të ri
        [HttpPost]
        public async Task<IActionResult> CreateTitle(string titleName, int fieldId)
        {
            // Krijon një titull të ri dhe kthen përgjigje HTTP OK
            await _titleService.CreateTitle(titleName, fieldId);
            return Ok();
        }

        // Metoda HTTP GET për të marrë të gjithë titujt
        [HttpGet("all")]
        public async Task<IActionResult> GetAllTitles()
        {
            // Merr të gjithë titujt dhe i kthen me përgjigje HTTP OK
            var titles = await _titleService.GetAllTitles();
            return Ok(titles);
        }

        // Metoda HTTP GET për të marrë titujt nga një fushë (field) specifike
        [HttpGet("{fieldName}")]
        public async Task<IActionResult> GetTitlesFromField(string fieldName)
        {
            // Merr titujt nga një fushë (field) specifike dhe i kthen me përgjigje HTTP OK
            var titles = await _titleService.GetTitlesFromField(fieldName);
            return Ok(titles);
        }

        // Metoda HTTP DELETE për të fshirë një titull specifik
        [HttpDelete("{titleName}")]
        public async Task<IActionResult> DeleteTitle(string titleName)
        {
            // Fshin titullin e specifikuar dhe kthen përgjigje HTTP OK
            await _titleService.DeleteTitle(titleName);
            return Ok();
        }
    }
}
