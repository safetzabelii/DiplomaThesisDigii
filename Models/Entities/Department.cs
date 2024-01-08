using System.Diagnostics;

namespace DiplomaThesisDigitalization.Models.Entities
{
    public class Department
    {
        public Department()
        {
            this.Mentors = new HashSet<Mentor>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Number { get; set; }

        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; }
        public virtual ICollection<Mentor> Mentors { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Field> Fields { get; set; }
    }
}
