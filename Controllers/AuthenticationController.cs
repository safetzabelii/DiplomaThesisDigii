using DiplomaThesisDigitalization.Models.DTOs;
using DiplomaThesisDigitalization.Models.Entities;
using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaThesisDigitalization.Controllers
{
    // Përcakton klasën AuthenticationController që menaxhon autentifikimin e përdoruesve.
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        // Shërbimi që ofron funksionalitete të lidhura me autentifikimin.
        private readonly IAuthenticationService _authenticationService;
        
        // Konstruktori, injekton shërbimin e autentifikimit.
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        // Endpoint për login të përdoruesit. Pret kredencialet e login dhe kthen një token JWT nëse është i suksesshëm.
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] LoginDTO login)
        {
            var jwt = await _authenticationService.Login(login);
            if (jwt == null)
            {
                // Nëse login është i pavlefshëm, kthen një përgjigje BadRequest.
                return BadRequest("Invalid login");
            }
            else 
            {
                // Nëse login është i suksesshëm, vendos një cookie me tokenin JWT dhe kthen një mesazh suksesi.
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

        // Endpoint për të marrë informacionin e përdoruesit të loguar bazuar në tokenin JWT.
        [HttpGet("logged-user")]
        public async Task<IActionResult> LoggedUser()
        {
            var jwt = Request.Cookies["jwt"];
            if (jwt == null)
            {
                // Nëse nuk gjendet asnjë token JWT, kthen një përgjigje BadRequest.
                return BadRequest("No logged user");
            }
            var loggedUser = await _authenticationService.LoggedUser(jwt);
            if (loggedUser != null)
            {
                // Nëse gjendet një përdorues, kthen informacionin e përdoruesit.
                return Ok(loggedUser);
            }
            // Nëse nuk gjendet asnjë përdorues, kthen një përgjigje NotFound.
            return NotFound();
        }
        
        // Endpoint për logout të përdoruesit. Fshin cookie-n JWT.
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
            
            // Fshin cookie-n 'jwt' për të bërë logout përdoruesin dhe kthen një mesazh suksesi.
            Response.Cookies.Delete("jwt", cookieOptions);
            return Ok(new
            {
                message = "Logout was successful"
            });
        }
    }
}
