using DiplomaThesisDigitalization.Data.UnitOfWork;
using DiplomaThesisDigitalization.Models.Entities;
using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace DiplomaThesisDigitalization.Services
{
    public class AdministratorService : IAdministratorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationService _authenticationService;
        public AdministratorService(IUnitOfWork unitOfWork, IAuthenticationService authenticationService)
        {
            _unitOfWork = unitOfWork;
            _authenticationService = authenticationService;
        }

        public async Task ApproveDiplomaThesisApplication(string jwt, int thesisApplicationId)
        {
            var loggedUser = _authenticationService.LoggedUser(jwt).Result;

            if (loggedUser is null || loggedUser.Role != "Admin")
            {
                throw new Exception("Duhet qasur admini per te aprovuar aplikimin e temes se diplomes");
            }

            var diplomaThesis = await _unitOfWork.Repository<DiplomaThesis>().GetByCondition(a => a.Id == thesisApplicationId).FirstOrDefaultAsync();

            if (diplomaThesis is null)
            {
                throw new Exception("Aplikimi i temes se diplomes me kete ID nuk ekziston");
            }

            if(diplomaThesis.DueDate != null)
            {
                throw new Exception("Ky aplikim eshte aprovuar me heret");
            }

            diplomaThesis.DueDate = DateTime.Now.AddMonths(6);
            _unitOfWork.Repository<DiplomaThesis>().Update(diplomaThesis);
            await _unitOfWork.SaveAsync();
        }

        public async Task DiplomaThesisSubmitted(string jwt, int thesisApplicationId)
        {
            var loggedUser = _authenticationService.LoggedUser(jwt).Result;

            if (loggedUser is null || loggedUser.Role != "Admin")
            {
                throw new Exception("Duhet qasur admini per te vertetuar dorezimin e temes se diplomes");
            }

            var diplomaThesis = await _unitOfWork.Repository<DiplomaThesis>().GetByCondition(a => a.Id == thesisApplicationId).FirstOrDefaultAsync();

            if (diplomaThesis is null)
            {
                throw new Exception("Aplikimi i temes se diplomes me kete ID nuk ekziston");
            }

            if (diplomaThesis.DueDate is null)
            {
                throw new Exception("Ky aplikim i temes se diplomes nuk eshte aprovuar");
            }

            if (diplomaThesis.DueDate < DateTime.Now)
            {
                throw new Exception("Ka kaluar afati per dorezimin e temes");
            }

            diplomaThesis.SubmissionDate = DateTime.Now;

            _unitOfWork.Repository<DiplomaThesis>().Update(diplomaThesis);
            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveDiplomaThesisApplication(string jwt, int thesisApplicationId)
        {
            var loggedUser = _authenticationService.LoggedUser(jwt).Result;

            if (loggedUser is null || loggedUser.Role != "Admin")
            {
                throw new Exception("Duhet qasur admini per te vertetuar dorezimin e temes se diplomes");
            }

            var diplomaThesis = await _unitOfWork.Repository<DiplomaThesis>().GetByCondition(a => a.Id == thesisApplicationId).FirstOrDefaultAsync();

            if (diplomaThesis is null)
            {
                throw new Exception("Aplikimi i temes se diplomes me kete ID nuk ekziston");
            }

            _unitOfWork.Repository<DiplomaThesis>().Delete(diplomaThesis);
            await _unitOfWork.SaveAsync();
        }

        public async Task SetThesisDueDate(string jwt, int thesisApplicationId, DateTime date)
        {
            var loggedUser = _authenticationService.LoggedUser(jwt).Result;

            if (loggedUser is null || loggedUser.Role != "Admin")
            {
                throw new Exception("Duhet qasur admini per te vertetuar dorezimin e temes se diplomes");
            }

            var diplomaThesis = await _unitOfWork.Repository<DiplomaThesis>().GetByCondition(a => a.Id == thesisApplicationId).FirstOrDefaultAsync();

            if (diplomaThesis is null)
            {
                throw new Exception("Aplikimi i temes se diplomes me kete ID nuk ekziston");
            }

            diplomaThesis.DueDate = date;
            _unitOfWork.Repository<DiplomaThesis>().Update(diplomaThesis);
            await _unitOfWork.SaveAsync();
        }
    }
}
