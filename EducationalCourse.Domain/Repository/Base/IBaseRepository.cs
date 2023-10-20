namespace EducationalCourse.Domain.ICommandRepositories.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> FetchIQueryableEntity();

        TEntity GetById(int id);

        Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);

        Task AddRangeAsync(IList<TEntity> entity, CancellationToken cancellationToken);

        void Delete(int id);

        void DeleteRange(IList<int> ids);

        void LogicalDelete(int id);

        void Update(TEntity entity);
    }
}
