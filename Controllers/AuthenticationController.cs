using DiplomaThesisDigitalization.Models.DTOs;
using DiplomaThesisDigitalization.Models.Entities;
using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaThesisDigitalization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }


        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] LoginDTO login)
        {
            var jwt = await _authenticationService.Login(login);
            if (jwt == null)
            {
                return BadRequest("Invalid login");
            }else {
                Response.Cookies.Append("jwt", jwt, new CookieOptions {
                     HttpOnly = true,
                      Domain = "localhost",
                      SameSite = SameSiteMode.None, 
                    Secure = true });
                return Ok(new
                {
                    message = "Login was successful"
                });
            }
        }

        [HttpGet("logged-user")]
        public async Task<IActionResult> LoggedUser()
        {
            var jwt = Request.Cookies["jwt"];
            if (jwt == null)
            {
                return BadRequest("No logged user");
            }
            var loggedUser = await _authenticationService.LoggedUser(jwt);
            if (loggedUser != null)
            {
                return Ok(loggedUser);
            }
            return NotFound();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            var cookieOptions = new CookieOptions
            {
                Domain = "localhost",
                Path = "/", 
                SameSite = SameSiteMode.None,
                Secure = true,
                HttpOnly = true
            };
            
            Response.Cookies.Delete("jwt", cookieOptions);
            return Ok(new
            {
                message = "Logout was successful"
            });
        }
    }
}
