namespace DiplomaThesisDigitalization.Models.Entities
{
    public class Field
    {
        public Field()
        {
            this.Mentors = new HashSet<Mentor>();
        }
        public int Id { get; set; }
        public string FieldName { get; set; }
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<Student> Students { get; set; }
        public ICollection<Title> Titles { get; set; }
        public ICollection<Mentor> Mentors { get; set; }
    }
}
