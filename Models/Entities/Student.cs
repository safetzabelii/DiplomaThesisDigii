using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Diagnostics;

namespace DiplomaThesisDigitalization.Models.Entities
{
    public class Student
    {
        [Key, ForeignKey("User")]
        public int Id { get; set; }
        public int ECTS { get; set; }
        public string DegreeLevel { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public int? DiplomaThesisId { get; set; }

        public DiplomaThesis DiplomaThesis { get; set; }

        public int FieldId { get; set; }  // Specializimi ~~ Field
        public Field Field { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
