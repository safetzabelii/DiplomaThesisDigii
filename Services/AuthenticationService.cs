using DiplomaThesisDigitalization.Data.UnitOfWork;
using DiplomaThesisDigitalization.Helpers;
using DiplomaThesisDigitalization.Models.DTOs;
using DiplomaThesisDigitalization.Models.Entities;
using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiplomaThesisDigitalization.Services
{
    public class AuthenticationService : IServices.IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly JwtHelper _jwtHelper;
        public AuthenticationService(IUnitOfWork unitOfWork, IUserService userService, JwtHelper jwtHelper)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _jwtHelper = jwtHelper;
        }


        public async Task<string> Login([FromBody] LoginDTO loginDTO)
        {
            var user = await _userService.GetUserFromEmail(loginDTO.Email);
            if(user == null)
            {
                throw new ArgumentException("Nuk ka user me kete email");
            }
            if(!BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password))
            {
                throw new ArgumentException("Passwordi nuk eshte i sakte");
            }

            return _jwtHelper.Generate(user.Id);
        }

        public async Task<User> LoggedUser(string jwt)
        {
            var token = _jwtHelper.Verify(jwt);
            int userId = int.Parse(token.Issuer);
            return await _userService.GetUserFromId(userId);
        }

    }
}
