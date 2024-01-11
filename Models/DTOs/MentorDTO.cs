public class MentorDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Status { get; set; }
    public string Availability { get; set; }
    public List<int> FieldIds { get; set; }
    public List<int> DepartmentIds { get; set; }

    public MentorDTO()
    {
        FieldIds = new List<int>();
        DepartmentIds = new List<int>();
    }
}
