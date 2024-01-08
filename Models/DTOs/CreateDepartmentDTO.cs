namespace DiplomaThesisDigitalization.Models.DTOs
{
    public class CreateDepartmentDTO
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int Number { get; set; }
        public int FacultyId { get; set; }
    }
}
