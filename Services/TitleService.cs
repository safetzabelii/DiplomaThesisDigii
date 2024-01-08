using DiplomaThesisDigitalization.Data.UnitOfWork;
using DiplomaThesisDigitalization.Models.Entities;
using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace DiplomaThesisDigitalization.Services
{
    public class TitleService : ITitleService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TitleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateTitle(string titleName, int fieldId)
        {
            var existingTitle = await _unitOfWork.Repository<Title>().GetById(a => a.TitleName == titleName).FirstOrDefaultAsync();
            if (existingTitle != null) 
            {
                throw new Exception("Ky titull ekziston");
            }
            var existingField = await _unitOfWork.Repository<Field>().GetById(a => a.Id == fieldId).FirstOrDefaultAsync();
            if (existingField == null)
            {
                throw new Exception("Kjo fushe nuk ekziston");
            }

            Title titull = new Title()
            {
                TitleName = titleName,
                FieldId = fieldId
            };

            await _unitOfWork.Repository<Title>().CreateAsync(titull);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteTitle(string titleName)
        {
            var existingTitle = await _unitOfWork.Repository<Title>().GetById(a => a.TitleName == titleName).FirstOrDefaultAsync();
            if (existingTitle == null)
            {
                throw new Exception("Ky nuk ekziston titull ekziston");
            }

            _unitOfWork.Repository<Title>().Delete(existingTitle);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<Title>> GetAllTitles()
        {
            return await _unitOfWork.Repository<Title>().GetAll().ToListAsync();
        }

        public async Task<List<Title>> GetTitlesFromField(string fieldName)
        {
            var existingField = await _unitOfWork.Repository<Field>().GetById(a => a.FieldName == fieldName).FirstOrDefaultAsync();
            if (existingField == null)
            {
                throw new Exception("Kjo fushe nuk ekziston");
            }
            return await _unitOfWork.Repository<Title>().GetAll().Where(a=>a.FieldId == existingField.Id).ToListAsync();
        }
    }
}
