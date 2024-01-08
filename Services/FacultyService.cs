using DiplomaThesisDigitalization.Data.UnitOfWork;
using DiplomaThesisDigitalization.Models.Entities;
using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.EntityFrameworkCore;
 
namespace DiplomaThesisDigitalization.Services
{
    public class FacultyService : IFacultyService
    {
        private readonly IUnitOfWork _unitOfWork;
        public FacultyService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateFaculty(string facultyName)
        {
            var repository = _unitOfWork.Repository<Faculty>();

            var existingFaculty = repository.GetAll().Where(a => a.Name == facultyName).FirstOrDefault();
            if (existingFaculty != null)
            {
                throw new ArgumentException("Fakulteti me kete emer ekziston!");
            }
            Faculty faculty = new Faculty() { Name = facultyName };
            await repository.CreateAsync(faculty);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteFaculty(string facultyName)
        {
            var repository = _unitOfWork.Repository<Faculty>();

            var existingFaculty = repository.GetAll().Where(a => a.Name == facultyName).FirstOrDefault();
            if (existingFaculty == null)
            {
                throw new ArgumentException("Fakulteti me kete emer nuk ekziston!");
            }
            repository.Delete(existingFaculty);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<Faculty>> GetAllFaculties()
        {
            var repository = _unitOfWork.Repository<Faculty>();

            return await repository.GetAll().ToListAsync();
        }
    }
}
