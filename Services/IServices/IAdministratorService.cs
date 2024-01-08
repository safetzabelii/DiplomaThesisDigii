namespace DiplomaThesisDigitalization.Services.IServices
{
    public interface IAdministratorService
    {
        Task ApproveDiplomaThesisApplication(string jwt, int thesisApplicationId);
        Task RemoveDiplomaThesisApplication(string jwt, int thesisApplicationId);
        Task DiplomaThesisSubmitted(string jwt, int thesisApplicationId);
        Task SetThesisDueDate(string jwt, int thesisApplicationId, DateTime date);

    }
}
