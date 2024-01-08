using DiplomaThesisDigitalization.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiplomaThesisDigitalization.Data
{
    public class ThesisDbContext : DbContext
    {
        public ThesisDbContext(DbContextOptions<ThesisDbContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Mentor> Mentors { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<DiplomaThesis> DiplomaTheses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ConsultationSchedule> ConsultationSchedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
            .HasOne<DiplomaThesis>(s => s.DiplomaThesis)
            .WithOne(dt => dt.Student)
            .HasForeignKey<DiplomaThesis>(dt => dt.StudentId);
        }
    }
}
