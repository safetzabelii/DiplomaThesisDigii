using System.Collections;
using DiplomaThesisDigitalization.Models.Entities;

namespace DiplomaThesisDigitalization.Services.IServices
{
    public interface IAdministratorService
    {
        Task<IEnumerable<DiplomaThesisDTO>> GetAllThesis();
        Task ApproveDiplomaThesisApplication(string jwt, int thesisApplicationId);
        Task RemoveDiplomaThesisApplication(string jwt, int thesisApplicationId);
        Task DiplomaThesisSubmitted(string jwt, int thesisApplicationId);
        Task SetThesisDueDate(string jwt, int thesisApplicationId, DateTime date);

    }
}
