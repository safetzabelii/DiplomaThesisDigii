using DiplomaThesisDigitalization.Data.UnitOfWork;
using DiplomaThesisDigitalization.Models.DTOs;
using DiplomaThesisDigitalization.Models.Entities;
using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;

namespace DiplomaThesisDigitalization.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateDepartment(CreateDepartmentDTO depDTO)
        {
            var repository = _unitOfWork.Repository<Department>();

            var existingDepartment = repository.GetAll().Where(a => a.Name == depDTO.Name).FirstOrDefault();
            if (existingDepartment != null)
            {
                throw new ArgumentException("Departamenti me kete emer ekziston!");
            }
            var existingFaculty = _unitOfWork.Repository<Faculty>().GetById(a => a.Id == depDTO.FacultyId).FirstOrDefault();
            if (existingFaculty == null)
            {
                throw new ArgumentException("Fakulteti me kete ID nuk ekziston");
            }

            Department dep = new Department() 
            { 
                Name = depDTO.Name, 
                FacultyId = depDTO.FacultyId,
                Location = depDTO.Location,
                Number = depDTO.Number
            };
            await repository.CreateAsync(dep);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteDepartment(int departmentId)
        {
            var repository = _unitOfWork.Repository<Department>();

            var existingDepartment = repository.GetById(d => d.Id == departmentId).FirstOrDefault();
            if(existingDepartment == null)
            {
                throw new ArgumentException("Departamenti me kete ID nuk ekziston");
            }
            repository.Delete(existingDepartment);
            await _unitOfWork.CompleteAsync();
        }   

        public async Task<List<Department>> GetAllDepartments()
        {
            var repository = _unitOfWork.Repository<Department>();

            return await repository.GetAll().ToListAsync();
        }

        public async Task<List<Department>> GetDepartmentsByFaculty(int facultyId)
        {
            var repository = _unitOfWork.Repository<Department>();

            return await repository.GetAll().Where(d => d.FacultyId == facultyId).ToListAsync();
        }
    }
}
