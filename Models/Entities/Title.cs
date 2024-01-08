using System.Diagnostics;

namespace DiplomaThesisDigitalization.Models.Entities
{
    public class Title
    {
        public int Id { get; set; }
        public string TitleName { get; set; }
        public int FieldId { get; set; }
        public Field Field { get; set; }
    }
}
