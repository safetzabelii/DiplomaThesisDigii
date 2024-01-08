using DiplomaThesisDigitalization.Models.DTOs;
using DiplomaThesisDigitalization.Models.Entities;

namespace DiplomaThesisDigitalization.Services.IServices
{
    public interface IAuthenticationService
    {
        Task<string> Login(LoginDTO loginDTO);
        Task<User> LoggedUser(string jwt);
    }
}
