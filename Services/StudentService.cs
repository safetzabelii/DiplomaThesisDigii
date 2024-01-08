using DiplomaThesisDigitalization.Data.UnitOfWork;
using DiplomaThesisDigitalization.Models.Entities;
using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace DiplomaThesisDigitalization.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationService _authenticationService;
        public StudentService(IUnitOfWork unitOfWork, IAuthenticationService authenticationService)
        {
            _unitOfWork = unitOfWork;
            _authenticationService = authenticationService;
        }
        public async Task SubmitThesisApplication(string jwt, string titleName, int mentorId)
        {
            var loggedUser = _authenticationService.LoggedUser(jwt).Result;

            if(loggedUser is null || loggedUser.Role != "Student")
            {
                throw new Exception("Duhet qasur studenti per te kryer aplikimin e temes se diplomes");
            }

            var student = await _unitOfWork.Repository<Student>().GetByCondition(a => a.Id == loggedUser.Id).FirstOrDefaultAsync();

            var mentor = await _unitOfWork.Repository<Mentor>().GetById(a => a.Id == mentorId).FirstOrDefaultAsync();
            var title = await _unitOfWork.Repository<Title>().GetByCondition(a => a.TitleName == titleName).FirstOrDefaultAsync();
            if(title is null)
            {
                throw new Exception("Ky titull nuk ekziston");
            }
            if(mentor is null)
            {
                throw new Exception("Mentori me kete ID nuk ekziston");
            }

            if(title.FieldId != student.FieldId)
            {
                throw new Exception("Fusha e temes se zgjedhur te diplomes duhet te jete e njejte me specializimin e studentit");
            }
            if (!mentor.Fields.Contains(title.Field))
            {
                throw new Exception("Mentori duhet te jete i specializuar ne fushen e kesaj teme te diplomes");
            }

            DiplomaThesis tema = new DiplomaThesis
            {
                StudentId = student.Id,
                MentorId= mentorId,
                TitleID= title.Id,
                Assessment = null,
                DueDate = null,
                SubmissionDate= null,
                Level = student.DegreeLevel
            };
            await _unitOfWork.Repository<DiplomaThesis>().CreateAsync(tema);
            await _unitOfWork.SaveAsync();
        }
        public async Task CancelThesisApplication(string jwt, int thesisApplicationId)
        {
            var loggedUser = _authenticationService.LoggedUser(jwt).Result;

            if (loggedUser is null || loggedUser.Role != "Student")
            {
                throw new Exception("Duhet qasur studenti per te anuluar aplikimin e temes se diplomes");
            }

            var thesisApplication = await _unitOfWork.Repository<DiplomaThesis>().GetById(a => a.Id == thesisApplicationId).FirstOrDefaultAsync();
            
            if(thesisApplication is null)
            {
                throw new Exception("Aplikacioni i temese se diplomes me kete ID nuk ekziston");
            }

            _unitOfWork.Repository<DiplomaThesis>().Delete(thesisApplication);
            await _unitOfWork.SaveAsync();
        }
    }
}
