using DiplomaThesisDigitalization.Services;
using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaThesisDigitalization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
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

        [HttpPut("approve/{thesisApplicationId}")]
        public async Task<IActionResult> ApproveDiplomaThesisApplication(int thesisApplicationId)
        {
            var jwt = Request.Cookies["jwt"];
            if (jwt == null)
            {
                return BadRequest("No logged user");
            }

            await _administratorService.ApproveDiplomaThesisApplication(jwt, thesisApplicationId);
            return Ok();
        }

        [HttpPut("submitted/{thesisApplicationId}")]
        public async Task<IActionResult> DiplomaThesisSubmitted(int thesisApplicationId)
        {
            var jwt = Request.Cookies["jwt"];
            if (jwt == null)
            {
                return BadRequest("No logged user");
            }
            await _administratorService.DiplomaThesisSubmitted(jwt, thesisApplicationId);
            return Ok();
        }

        [HttpDelete("remove/{thesisApplicationId}")]
        public async Task<IActionResult> RemoveDiplomaThesisApplication(int thesisApplicationId)
        {
            var jwt = Request.Cookies["jwt"];
            if (jwt == null)
            {
                return BadRequest("No logged user");
            }
            await _administratorService.RemoveDiplomaThesisApplication(jwt, thesisApplicationId);
            return Ok();
        }

        [HttpDelete("setDate")]
        public async Task<IActionResult> SetThesisDueDate(int thesisApplicationId, DateTime date)
        {
            var jwt = Request.Cookies["jwt"];
            if (jwt == null)
            {
                return BadRequest("No logged user");
            }
            await _administratorService.SetThesisDueDate(jwt, thesisApplicationId, date);
            return Ok();
        }
    }
}
