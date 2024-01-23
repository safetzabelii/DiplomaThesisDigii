public class DiplomaThesisDTO
{
    public int Id { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? SubmissionDate { get; set; }
    public byte? Assessment { get; set; }
    public string Level { get; set; }
    public string StudentName { get; set; }
    public string MentorName { get; set; }
    public string TitleName { get; set; }
}
