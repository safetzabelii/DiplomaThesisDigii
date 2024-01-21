using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DiplomaThesisDigitalization.Models.Entities
{
    public class Mentor
    {
        public Mentor()
        {
            Fields = new HashSet<Field>();
            Departments = new HashSet<Department>();
        }
        [Key, ForeignKey("User")]
        public int Id { get; set; }
        public string Status { get; set; }
        public string Availability { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public virtual ICollection<Field> Fields { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}
