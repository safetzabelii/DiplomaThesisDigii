namespace DiplomaThesisDigitalization.Models.Entities
{
    public class ConsultationSchedule
    {
        public int Id { get; set; }
        public int DiplomaThesisId { get; set; }
        public DiplomaThesis DiplomaThesis { get; set; }
        public DateTime ConsultationDate { get; set; }
        public string ConsultationPlace { get; set; }
    }
}
