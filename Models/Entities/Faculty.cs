﻿namespace DiplomaThesisDigitalization.Models.Entities
{
    public class Faculty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}
