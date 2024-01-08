using DiplomaThesisDigitalization.Models.Entities;

namespace DiplomaThesisDigitalization.Services.IServices
{
    public interface IFieldService
    {
        Task<List<Field>> GetAllFields();
        Task CreateField(string fieldName, int departmentId);
        Task DeleteField(string fieldName);
        Task<List<User>> GetMentorsFromField(string fieldName);
        Task<List<User>> GetStudentsFromField(string fieldName);

    }
}
