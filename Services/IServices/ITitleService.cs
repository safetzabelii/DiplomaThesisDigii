using DiplomaThesisDigitalization.Models.Entities;

namespace DiplomaThesisDigitalization.Services.IServices
{
    public interface ITitleService
    {
        Task<List<Title>> GetAllTitles();
        Task CreateTitle(string titleName, int fieldId);
        Task DeleteTitle(string fieldName);
        Task<List<Title>> GetTitlesFromField(string fieldName);
    }
}
