using DiplomaThesisDigitalization.Models.Entities;

namespace DiplomaThesisDigitalization.Models.DTOs
{
    public class CreateStudentDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int ECTS { get; set; }
        public string DegreeLevel { get; set; }
        public int FieldId { get; set; }  // Specializimi ~~ Field
        //public Field Field { get; set; }
        public int DepartmentId { get; set; }
        //public Department Department { get; set; }
    }
}
