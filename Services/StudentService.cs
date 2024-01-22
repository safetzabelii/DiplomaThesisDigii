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
            public async Task<int?> GetCurrentThesisId(string jwt)
            {
                var loggedUser = await _authenticationService.LoggedUser(jwt);

                if (loggedUser is null || loggedUser.Role != "Student")
                {
                    throw new Exception("Duhet qasur studenti per te shfaqur temen e diplomes aktuale");
                }

                var student = await _unitOfWork.Repository<Student>().GetByCondition(a => a.Id == loggedUser.Id).FirstOrDefaultAsync();

                var currentThesis = await _unitOfWork.Repository<DiplomaThesis>()
                    .GetById(dt => dt.StudentId == student.Id)
                    .FirstOrDefaultAsync();

                return currentThesis?.Id;
            }
            public async Task<CurrentThesisDTO> GetCurrentThesis(string jwt)
            {
                var loggedUser = await _authenticationService.LoggedUser(jwt);
                if (loggedUser is null || loggedUser.Role != "Student")
                {
                    throw new Exception("Only students can access current thesis details.");
                }

                var student = await _unitOfWork.Repository<Student>()
                            .GetByCondition(a => a.Id == loggedUser.Id)
                            .FirstOrDefaultAsync();
               

                var currentThesis = await _unitOfWork.Repository<DiplomaThesis>()
                                .GetByCondition(dt => dt.StudentId == student.Id)
                                .Include(dt => dt.Title)
                                .Include(dt => dt.Mentor)
                                .FirstOrDefaultAsync();
                if (currentThesis == null)
                {
                    return null;
                }

                return  new CurrentThesisDTO
                {
                    Id = currentThesis.Id,
                    TitleName = currentThesis.Title?.TitleName,
                    MentorName =currentThesis.Mentor?.User?.Name,
                    DueDate = currentThesis.DueDate,
                    SubmissionDate = currentThesis.SubmissionDate,
                    Assessment = currentThesis.Assessment,
                    Level = currentThesis.Level,
                    StudentName = currentThesis.Student.User.Name,
                    
                };
            }
        
        public async Task<int> SubmitThesisApplication(string jwt, string titleName, int mentorId)
        {
            var loggedUser = await _authenticationService.LoggedUser(jwt);

            if (loggedUser is null || loggedUser.Role != "Student")
            {
                throw new Exception("Duhet qasur studenti per te kryer aplikimin e temes se diplomes");
            }

            var student = await _unitOfWork.Repository<Student>().GetByCondition(a => a.Id == loggedUser.Id).FirstOrDefaultAsync();

            var mentor = await _unitOfWork.Repository<Mentor>().GetById(a => a.Id == mentorId).FirstOrDefaultAsync();
            var title = await _unitOfWork.Repository<Title>().GetByCondition(a => a.TitleName == titleName).FirstOrDefaultAsync();
            if (title is null)
            {
                throw new Exception("Ky titull nuk ekziston");
            }
            if (mentor is null)
            {
                throw new Exception("Mentori me kete ID nuk ekziston");
            }

            if (title.FieldId != student.FieldId)
            {
                throw new Exception("Fusha e temes se zgjedhur te diplomes duhet te jete e njejte me specializimin e studentit");
            }
            if (mentor.Fields.Any(f => f.Id == title.FieldId))
            {
                throw new Exception("Mentori duhet te jete i specializuar ne fushen e kesaj teme te diplomes");
            }

            var existingThesis = await _unitOfWork.Repository<DiplomaThesis>()
                                    .GetByCondition(dt => dt.StudentId == student.Id)
                                    .FirstOrDefaultAsync();
            if (existingThesis != null)
            {
                throw new Exception("Studenti ka nje teme te diplomes ne proces");
            }

            DiplomaThesis tema = new DiplomaThesis
            {
                StudentId = student.Id,
                MentorId = mentorId,
                TitleID = title.Id,
                Assessment = null,
                DueDate = null,
                SubmissionDate = null,
                Level = student.DegreeLevel
            };

            await _unitOfWork.Repository<DiplomaThesis>().CreateAsync(tema);
            await _unitOfWork.SaveAsync();

            return tema.Id;
        }

        public async Task<int?> CancelThesisApplication(string jwt, int thesisApplicationId)
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

                return thesisApplication.Id;
            }
    }
}
