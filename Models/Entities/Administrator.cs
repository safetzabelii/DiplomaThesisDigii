using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DiplomaThesisDigitalization.Models.Entities
{
    public class Administrator
    {
        [Key, ForeignKey("User")]
        public int Id { get; set; }
        public string Type { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
