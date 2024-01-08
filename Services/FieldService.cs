using DiplomaThesisDigitalization.Data.UnitOfWork;
using DiplomaThesisDigitalization.Models.Entities;
using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace DiplomaThesisDigitalization.Services
{
    public class FieldService : IFieldService
    {
        private readonly IUnitOfWork _unitOfWork;
        public FieldService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateField(string fieldName, int departmentId)
        {
            var repository = _unitOfWork.Repository<Field>();
            
            var existingField = repository.GetAll().Where(a => a.FieldName== fieldName).FirstOrDefault();
            if (existingField != null)
            {
                throw new ArgumentException("Fusha me kete emer ekziston!");
            }
            Field field = new Field() { FieldName = fieldName, DepartmentId = departmentId };
            await repository.CreateAsync(field);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteField(string fieldName)
        {
            var repository = _unitOfWork.Repository<Field>();

            var existingField = repository.GetAll().Where(a => a.FieldName == fieldName).FirstOrDefault();
            if (existingField == null)
            {
                throw new ArgumentException("Fusha me kete emer nuk ekziston!");
            }
            repository.Delete(existingField);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<Field>> GetAllFields()
        {
            var repository = _unitOfWork.Repository<Field>();

            return await repository.GetAll().ToListAsync();
        }

        public async Task<List<User>> GetMentorsFromField(string fieldName)
        {
            List<User> users= new List<User>();

            var field = await _unitOfWork.Repository<Field>().GetById(a => a.FieldName == fieldName).FirstOrDefaultAsync();

            if (field == null)
            {
                throw new Exception("Fusha me kete emer nuk ekziston");
            }

            var mentors = _unitOfWork.Repository<Mentor>().GetAll().Where(a => a.Fields.Contains(field)).ToListAsync().Result;

            foreach(var ment in mentors)
            {
                var user = await _unitOfWork.Repository<User>().GetById(a => a.Id == ment.Id).FirstOrDefaultAsync();
                users.Add(user);
            }

            return users;
        }

        public async Task<List<User>> GetStudentsFromField(string fieldName)
        {
            List<User> users = new List<User>();

            var field = await _unitOfWork.Repository<Field>().GetById(a => a.FieldName == fieldName).FirstOrDefaultAsync();

            if (field == null)
            {
                throw new Exception("Fusha me kete emer nuk ekziston");
            }

            var students = _unitOfWork.Repository<Student>().GetAll().Where(a => a.FieldId == field.Id).ToListAsync().Result;

            foreach (var stud in students)
            {
                var user = await _unitOfWork.Repository<User>().GetById(a => a.Id == stud.Id).FirstOrDefaultAsync();
                users.Add(user);
            }

            return users;
        }
    }
}
 
