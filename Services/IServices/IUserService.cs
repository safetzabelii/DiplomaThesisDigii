using DiplomaThesisDigitalization.Models.DTOs;
using DiplomaThesisDigitalization.Models.Entities;

namespace DiplomaThesisDigitalization.Services.IServices
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUserFromId(int userId);
        Task<User> GetUserFromEmail(string userEmail);
        Task AddStudent(CreateStudentDTO studentDTO);
        Task<List<StudentDTO>> GetAllStudentsAsync();
        Task DeleteStudent(int studentId);
        Task AddMentor(CreateMentorDTO mentorDTO);
        Task DeleteMentor(int mentorId);
        Task AddAdmin(CreateAdminDTO adminDTO);
        Task DeleteAdmin(int adminId);
    }
}
