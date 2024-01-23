using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DiplomaThesisDigitalization.Models.Entities
{
    public class DiplomaThesis
    {
        public int Id { get; set; }
       
        public DateTime? DueDate { get; set; } = null;
        public DateTime? SubmissionDate { get; set; } = null;
        public byte? Assessment { get; set; }
        public string Level { get; set; }

        [ForeignKey("Student")]
        public int? StudentId { get; set; }
        
        [JsonIgnore]
        public Student Student { get; set; }

        [ForeignKey("Mentor")]
        public int MentorId { get; set; }
        [JsonIgnore]
        public Mentor Mentor { get; set; }
        
        public int TitleID { get; set; }
        public Title Title { get; set; }

        public ICollection<ConsultationSchedule> ConsultationSchedules { get; set; }
    }
}
