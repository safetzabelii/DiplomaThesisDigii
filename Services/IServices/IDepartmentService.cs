using DiplomaThesisDigitalization.Models.DTOs;
using DiplomaThesisDigitalization.Models.Entities;

namespace DiplomaThesisDigitalization.Services.IServices
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetAllDepartments();
        Task<List<Department>> GetDepartmentsByFaculty(int facultyId);
        Task CreateDepartment(CreateDepartmentDTO departmentDto);
        Task DeleteDepartment(int departmentId);
    }
}
