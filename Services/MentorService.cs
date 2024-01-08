using DiplomaThesisDigitalization.Data.UnitOfWork;
using DiplomaThesisDigitalization.Models.Entities;
using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace DiplomaThesisDigitalization.Services
{
    public class MentorService : IMentorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationService _authenticationService;
        public MentorService(IUnitOfWork unitOfWork, IAuthenticationService authenticationService)
        {
            _unitOfWork = unitOfWork;
            _authenticationService = authenticationService;
        }

        public async Task AddDepartment(string jwt, int departmentId)
        {
            var loggedUser = _authenticationService.LoggedUser(jwt).Result;

            if (loggedUser is null || loggedUser.Role != "Mentor")
            {
                throw new Exception("Duhet qasur mentori per te shtuar nje departament ne te cilin punon");
            }

            var department = await _unitOfWork.Repository<Department>().GetById(a => a.Id == departmentId).FirstOrDefaultAsync();
        
            if(department is null)
            {
                throw new Exception("Departamenti me kete ID nuk ekziston");
            }


            var mentor = await _unitOfWork.Repository<Mentor>().GetById(a => a.Id == loggedUser.Id).FirstOrDefaultAsync();
            
            if (mentor.Departments.Contains(department))
            {
                throw new Exception("Ky mentor punon ne kete departament");
            }

            mentor.Departments.Add(department);
            _unitOfWork.Repository<Mentor>().Update(mentor);
            await _unitOfWork.SaveAsync();
        }

        public async Task AddField(string jwt, int fieldId)
        {
            var loggedUser = _authenticationService.LoggedUser(jwt).Result;

            if (loggedUser is null || loggedUser.Role != "Mentor")
            {
                throw new Exception("Duhet qasur mentori per te shtuar nje fushe ne te cilen specializohet");
            }

            var field = await _unitOfWork.Repository<Field>().GetById(a => a.Id == fieldId).FirstOrDefaultAsync();

            if (field is null)
            {
                throw new Exception("Fusha me kete ID nuk ekziston");
            }


            var mentor = await _unitOfWork.Repository<Mentor>().GetById(a => a.Id == loggedUser.Id).FirstOrDefaultAsync();

            if (mentor.Fields.Contains(field))
            {
                throw new Exception("Ky mentor specializon ne kete fushe");
            }

            mentor.Fields.Add(field);
            _unitOfWork.Repository<Mentor>().Update(mentor);
            await _unitOfWork.SaveAsync();
        }

        public async Task AssessDiplomaThesis(string jwt, int diplomaThesisId, byte assessment)
        {
            var loggedUser = _authenticationService.LoggedUser(jwt).Result;

            if (loggedUser is null || loggedUser.Role != "Mentor")
            {
                throw new Exception("Duhet qasur mentori vleresuar temen e diplomes");
            }

            var thesis = await _unitOfWork.Repository<DiplomaThesis>().GetById(a => a.Id == diplomaThesisId).FirstOrDefaultAsync();
            var mentor = await _unitOfWork.Repository<Mentor>().GetById(a => a.Id == loggedUser.Id).FirstOrDefaultAsync();

            if(thesis is null)
            {
                throw new Exception("Tema e diplomes nuk ekziston");
            }

            thesis.Assessment = assessment;
            _unitOfWork.Repository<DiplomaThesis>().Update(thesis);
            await _unitOfWork.SaveAsync();

        }

        public async Task RemoveDepartment(string jwt, int departmentId)
        {
            var loggedUser = _authenticationService.LoggedUser(jwt).Result;

            if (loggedUser is null || loggedUser.Role != "Mentor")
            {
                throw new Exception("Duhet qasur mentori per te hequr nje departament ne te cilin punon");
            }

            var department = await _unitOfWork.Repository<Department>().GetById(a => a.Id == departmentId).FirstOrDefaultAsync();

            if (department is null)
            {
                throw new Exception("Departamenti me kete ID nuk ekziston");
            }


            var mentor = await _unitOfWork.Repository<Mentor>().GetById(a => a.Id == loggedUser.Id).FirstOrDefaultAsync();

            if (!mentor.Departments.Contains(department))
            {
                throw new Exception("Ky mentor nuk punon ne kete departament");
            }

            mentor.Departments.Remove(department);
            _unitOfWork.Repository<Mentor>().Update(mentor);
            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveField(string jwt, int fieldId)
        {
            var loggedUser = _authenticationService.LoggedUser(jwt).Result;

            if (loggedUser is null || loggedUser.Role != "Mentor")
            {
                throw new Exception("Duhet qasur mentori per te hequr nje fushe ne te cilen specializohet");
            }

            var field = await _unitOfWork.Repository<Field>().GetById(a => a.Id == fieldId).FirstOrDefaultAsync();

            if (field is null)
            {
                throw new Exception("Fusha me kete ID nuk ekziston");
            }


            var mentor = await _unitOfWork.Repository<Mentor>().GetById(a => a.Id == loggedUser.Id).FirstOrDefaultAsync();

            if (!mentor.Fields.Contains(field))
            {
                throw new Exception("Ky mentor nuk specializon ne kete fushe");
            }

            mentor.Fields.Remove(field);
            _unitOfWork.Repository<Mentor>().Update(mentor);
            await _unitOfWork.SaveAsync();
        }

        public async Task SetAvailability(string jwt, string availability)
        {
            var loggedUser = _authenticationService.LoggedUser(jwt).Result;

            if (loggedUser is null || loggedUser.Role != "Mentor")
            {
                throw new Exception("Duhet qasur mentori per te ndryshuar availability");
            }

            var mentor = await _unitOfWork.Repository<Mentor>().GetById(a => a.Id == loggedUser.Id).FirstOrDefaultAsync();

            mentor.Availability = availability;
            _unitOfWork.Repository<Mentor>().Update(mentor);
            await _unitOfWork.SaveAsync();
        }
    }
}
