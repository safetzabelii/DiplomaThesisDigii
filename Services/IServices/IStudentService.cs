namespace DiplomaThesisDigitalization.Services.IServices
{
    public interface IStudentService
    {
        Task<int> SubmitThesisApplication(string jwt, string title, int mentorId);
        Task<int?> CancelThesisApplication(string jwt, int thesisApplicationId);
        Task<int?> GetCurrentThesisId(string jwt);
        Task<CurrentThesisDTO>GetCurrentThesis(string jwt);
    }
}
