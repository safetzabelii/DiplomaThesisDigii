using System.Linq.Expressions;

namespace DiplomaThesisDigitalization.Data.ThesisRepository
{
    public class ThesisRepository<TEntity> : IThesisRepository<TEntity> where TEntity : class
    {
        private readonly ThesisDbContext _dbContext;

        public ThesisRepository(ThesisDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> expression)
        {
            return _dbContext.Set<TEntity>().Where(expression);
        }

         // Get all entities of type TEntity
        public IQueryable<TEntity> GetAll()
        {
            return  _dbContext.Set<TEntity>();  
        }

        
        public IQueryable<TEntity> GetById(Expression<Func<TEntity, bool>> expression)
        {
            return  _dbContext.Set<TEntity>().Where(expression);
        }

        public void Create(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }
        public async Task CreateAsync(TEntity entity)
        {
            await  _dbContext.Set<TEntity>().AddAsync(entity);
        }
        
        // Delete an existing entity
        public void Delete(TEntity entity)
        {
             _dbContext.Set<TEntity>().Remove(entity);
        }
        
        // Update an existing entity
        public void Update(TEntity entity)
        {
             _dbContext.Set<TEntity>().Update(entity);
        }
        
    }
}
