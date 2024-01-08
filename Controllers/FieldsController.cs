using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaThesisDigitalization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldsController : ControllerBase
    {
        private readonly IFieldService _fieldService;

        public FieldsController(IFieldService fieldService)
        {
            _fieldService = fieldService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateField(string fieldName, int departmentId)
        {
            try
            {
                await _fieldService.CreateField(fieldName, departmentId);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllFields()
        {
            try
            {
                var fields = await _fieldService.GetAllFields();
                return Ok(fields);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpDelete("{fieldName}")]
        public async Task<IActionResult> DeleteField(string fieldName)
        {
            try
            {
                await _fieldService.DeleteField(fieldName);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        [HttpGet("{fieldName}/mentors")]
        public async Task<IActionResult> GetMentorsFromField(string fieldName)
        {
            try
            {
                var fields = await _fieldService.GetMentorsFromField(fieldName);
                return Ok(fields);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet("{fieldName}/students")]
        public async Task<IActionResult> GetStudentsFromField(string fieldName)
        {
            try
            {
                var fields = await _fieldService.GetStudentsFromField(fieldName);
                return Ok(fields);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
