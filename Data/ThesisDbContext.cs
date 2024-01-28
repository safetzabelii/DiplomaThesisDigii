using DiplomaThesisDigitalization.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiplomaThesisDigitalization.Data
{
    // Definimi i klasës për ThesisDbContext, e cila trashëgon nga DbContext
    public class ThesisDbContext : DbContext
    {
        // Konstruktori që merr DbContextOptions si parameter dhe e kalon atë te konstruktori i klasës bazë
        public ThesisDbContext(DbContextOptions<ThesisDbContext> options) : base(options)
        {
            
        }

        //  DBSet "properties" që paraqesin tabelat në bazën e të dhënave 
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

        // Metoda për konfigurimin e lidhjeve dhe kufizimeve midis entiteteve në bazën e të dhënave
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Përcakto një lidhje një-me-një mes 'Student' dhe 'DiplomaThesis', me kufizime në "foreign key"
            modelBuilder.Entity<Student>()
            .HasOne<DiplomaThesis>(s => s.DiplomaThesis)
            .WithOne(dt => dt.Student)
            .HasForeignKey<DiplomaThesis>(dt => dt.StudentId);

        }
    }
}
