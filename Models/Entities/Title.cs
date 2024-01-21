using System.Diagnostics;
using System.Text.Json.Serialization;

namespace DiplomaThesisDigitalization.Models.Entities
{
    public class Title
    {
        public int Id { get; set; }
        public string TitleName { get; set; }
        [JsonIgnore]
        public Field Field { get; set; }
        public int FieldId { get; set; }
    }
}
