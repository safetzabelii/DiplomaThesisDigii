using DiplomaThesisDigitalization.Data.ThesisRepository;

namespace DiplomaThesisDigitalization.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IThesisRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task<bool> CompleteAsync();
        Task SaveAsync();
    }
}
