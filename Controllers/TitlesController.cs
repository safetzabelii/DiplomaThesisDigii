using DiplomaThesisDigitalization.Services;
using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaThesisDigitalization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitlesController : ControllerBase
    {
        private readonly ITitleService _titleService;
        public TitlesController(ITitleService titleService)
        {
            _titleService = titleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTitle(string titleName, int fieldId)
        {
            await _titleService.CreateTitle(titleName, fieldId);
            return Ok();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllTitles()
        {
            var titles = await _titleService.GetAllTitles();
            return Ok(titles);
        }

        [HttpGet("{fieldName}")]
        public async Task<IActionResult> GetTitlesFromField(string fieldName)
        {
            var titles = await _titleService.GetTitlesFromField(fieldName);
            return Ok(titles);
        }

        [HttpDelete("{titleName}")]
        public async Task<IActionResult> DeleteTitle(string titleName)
        {
            await _titleService.DeleteTitle(titleName);
            return Ok();
        }
    }
}
