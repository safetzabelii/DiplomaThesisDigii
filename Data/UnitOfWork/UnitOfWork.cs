using DiplomaThesisDigitalization.Data.ThesisRepository;
using System.Collections;

namespace DiplomaThesisDigitalization.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ThesisDbContext _dbContext;
        private Hashtable _repositories;

        public UnitOfWork(ThesisDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CompleteAsync()
        {
            int affectedRows = await _dbContext.SaveChangesAsync();
            return affectedRows > 0;
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public IThesisRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Hashtable();

            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(ThesisRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dbContext);

                _repositories.Add(type, repositoryInstance);
            }

            return (IThesisRepository<TEntity>)_repositories[type];
        }
    }
}
