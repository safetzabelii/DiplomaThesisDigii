﻿namespace DiplomaThesisDigitalization.Models.DTOs
{
    public class CreateAdminDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }
    }
}
