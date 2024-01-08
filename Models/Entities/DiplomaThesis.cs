using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomaThesisDigitalization.Models.Entities
{
    public class DiplomaThesis
    {
        [ForeignKey("Student")]
        public int Id { get; set; }
       
        public DateTime? DueDate { get; set; } = null;
        public DateTime? SubmissionDate { get; set; } = null;
        public byte? Assessment { get; set; }
        public string Level { get; set; }
        public int? StudentId { get; set; }
        public Student Student { get; set; }
        public int MentorId { get; set; }
        public Mentor Mentor { get; set; }
        public int TitleID { get; set; }
        public Title Title { get; set; }

        public ICollection<ConsultationSchedule> ConsultationSchedules { get; set; }
    }
}
