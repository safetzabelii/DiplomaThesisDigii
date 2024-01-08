namespace DiplomaThesisDigitalization.Services.IServices
{
    public interface IStudentService
    {
        Task SubmitThesisApplication(string jwt, string title, int mentorId);
        Task CancelThesisApplication(string jwt, int thesisApplicationId);
    }
}
