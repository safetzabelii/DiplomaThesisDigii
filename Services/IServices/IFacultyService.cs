using DiplomaThesisDigitalization.Models.Entities;

namespace DiplomaThesisDigitalization.Services.IServices
{
    public interface IFacultyService
    {
        Task<List<Faculty>> GetAllFaculties();
        Task CreateFaculty(string facultyName);
        Task DeleteFaculty(string facultyName);
    }
}
