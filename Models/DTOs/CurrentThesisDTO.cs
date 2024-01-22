public class CurrentThesisDTO
{
    public int Id { get; set; }
    public string TitleName { get; set; }
    public string MentorName { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? SubmissionDate { get; set; }
    public byte? Assessment { get; set; }
    public string Level { get; set; }
    public int? StudentId { get; set; }
    public string StudentName { get; set; }
    
    //public IEnumerable<ConsultationScheduleDTO> ConsultationDetails { get; set; }
}