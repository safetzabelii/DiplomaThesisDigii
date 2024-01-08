using DiplomaThesisDigitalization.Data.UnitOfWork;
using DiplomaThesisDigitalization.Models.DTOs;
using DiplomaThesisDigitalization.Models.Entities;
using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiplomaThesisDigitalization.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var repository = _unitOfWork.Repository<User>();

            return await repository.GetAll().ToListAsync();
        }

        public async Task<User> GetUserFromEmail(string userEmail)
        {
            var repository = _unitOfWork.Repository<User>();

            return await repository.GetByCondition(a => a.Email == userEmail).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserFromId(int userId)
        {
            var repository = _unitOfWork.Repository<User>();

            return await repository.GetByCondition(a => a.Id == userId).FirstOrDefaultAsync();
        }

        public async Task AddAdmin([FromQuery] CreateAdminDTO adminDTO)
        {
            if (await this.GetUserFromEmail(adminDTO.Email) != null)
            {
                throw new ArgumentException("Ekziston nje user me kete email");
            }

            User user = new User
            {
                Name = adminDTO.Name,
                Surname = adminDTO.Surname,
                DOB = adminDTO.DOB,
                Email = adminDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(adminDTO.Password),
                Gender = adminDTO.Gender,
                Phone = adminDTO.Phone,
                Address = adminDTO.Address,
                Role = "Admin"
            }; 

            await _unitOfWork.Repository<User>().CreateAsync(user);
            await _unitOfWork.SaveAsync();

            Administrator admin = new Administrator
            {
                Id = user.Id,
                Type = adminDTO.Type
            };

            await _unitOfWork.Repository<Administrator>().CreateAsync(admin);
            await _unitOfWork.CompleteAsync();
        }
        public async Task DeleteAdmin(int adminId)
        {

            var user = await this.GetUserFromId(adminId);
            if (user == null)
            {
                throw new ArgumentException("Nuk ekziston ndonje user me kete ID");
            }
            var admin = await _unitOfWork.Repository<Administrator>().GetByCondition(a => a.Id == adminId).FirstOrDefaultAsync();

            _unitOfWork.Repository<Administrator>().Delete(admin);
            _unitOfWork.Repository<User>().Delete(user);
            await _unitOfWork.CompleteAsync();
        }

        public async Task AddMentor([FromQuery] CreateMentorDTO mentorDTO)
        {
            if (await this.GetUserFromEmail(mentorDTO.Email) != null)
            {
                throw new ArgumentException("Ekziston nje user me kete email");
            }

            User user = new User
            {
                Name = mentorDTO.Name,
                Surname = mentorDTO.Surname,
                DOB = mentorDTO.DOB,
                Email = mentorDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(mentorDTO.Password),
                Gender = mentorDTO.Gender,
                Phone = mentorDTO.Phone,
                Address = mentorDTO.Address,
                Role = "Mentor"
            };

            await _unitOfWork.Repository<User>().CreateAsync(user);
            await _unitOfWork.SaveAsync();

            Mentor mentor = new Mentor
            {
                Id = user.Id,
                Availability = mentorDTO.Availability,
                Status = mentorDTO.Status,
            };

            await _unitOfWork.Repository<Mentor>().CreateAsync(mentor);
            await _unitOfWork.CompleteAsync();
        }
        public async Task DeleteMentor(int mentorId)
        {
            var user = await this.GetUserFromId(mentorId);
            if (user == null)
            {
                throw new ArgumentException("Nuk ekziston ndonje user me kete ID");
            }
            Mentor? mentor = await _unitOfWork.Repository<Mentor>().GetByCondition(a => a.Id == mentorId).FirstOrDefaultAsync();

            _unitOfWork.Repository<Mentor>().Delete(mentor);
            _unitOfWork.Repository<User>().Delete(user);
            await _unitOfWork.CompleteAsync();
        }

        public async Task AddStudent([FromQuery] CreateStudentDTO studentDTO)
        {
            if (await this.GetUserFromEmail(studentDTO.Email) != null)
            {
                throw new ArgumentException("Ekziston nje user me kete email");
            }

            User user = new User
            {
                Name = studentDTO.Name,
                Surname = studentDTO.Surname,
                DOB = studentDTO.DOB,
                Email = studentDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(studentDTO.Password),
                Gender = studentDTO.Gender,
                Phone = studentDTO.Phone,
                Address = studentDTO.Address,
                Role = "Student"
            };

            await _unitOfWork.Repository<User>().CreateAsync(user);
            await _unitOfWork.SaveAsync();

            Student student = new Student
            {
                Id = user.Id,
                ECTS = studentDTO.ECTS,
                DegreeLevel = studentDTO.DegreeLevel,
                FieldId = studentDTO.FieldId,
                DepartmentId = studentDTO.DepartmentId
            };

            student.Id = user.Id;
            await _unitOfWork.Repository<Student>().CreateAsync(student);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<StudentDTO>> GetAllStudentsAsync()
{
    var students = await _unitOfWork.Repository<Student>().GetAll().Include(s => s.User).ToListAsync();
    
    return students.Select(s => new StudentDTO
    {
        Id = s.Id,
        Name = s.User.Name,
        Surname = s.User.Surname,
        Email = s.User.Email,
        ECTS = s.ECTS,
        DegreeLevel = s.DegreeLevel,
        FieldId = s.FieldId,
        DepartmentId = s.DepartmentId
    }).ToList();
}



        public async Task DeleteStudent(int studentId)
        {
            var user = await this.GetUserFromId(studentId);
            if (user == null)
            {
                throw new ArgumentException("Nuk ekziston ndonje user me kete ID");
            }
            Student? student = await _unitOfWork.Repository<Student>().GetByCondition(a => a.Id == studentId).FirstOrDefaultAsync();

            _unitOfWork.Repository<Student>().Delete(student);
            _unitOfWork.Repository<User>().Delete(user);
            await _unitOfWork.CompleteAsync();
        }
    }
}
