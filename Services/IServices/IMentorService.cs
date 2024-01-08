namespace DiplomaThesisDigitalization.Services.IServices
{
    public interface IMentorService
    {
        Task SetAvailability(string jwt, string availability);
        Task AssessDiplomaThesis(string jwt, int diplomaThesisId, byte assessment);
        Task AddField(string jwt, int fieldId);
        Task RemoveField(string jwt, int fieldId);
        Task AddDepartment(string jwt, int departmentId);
        Task RemoveDepartment(string jwt, int departmentId);
    }
}
