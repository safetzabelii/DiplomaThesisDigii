using System.Linq.Expressions;

namespace DiplomaThesisDigitalization.Data.ThesisRepository
{
    public interface IThesisRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetById(Expression<Func<TEntity, bool>> expression);
        void Create(TEntity entity);
        Task CreateAsync(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}
