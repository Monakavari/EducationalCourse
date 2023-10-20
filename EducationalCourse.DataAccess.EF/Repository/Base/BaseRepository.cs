using EducationalCourse.DataAccess.EF.Context;
using EducationalCourse.Domain.ICommandRepositories.Base;
using EducationalCourse.Domain.Models.Base;

namespace EducationalCourse.DataAccess.EF.Repositories.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity, new()
    {
        #region Constractor

        protected EducationalCourseContext Context { get; }
        public BaseRepository(EducationalCourseContext context)
        {
            Context = context;
        }

        #endregion Constractor

        public IQueryable<TEntity> FetchIQueryableEntity()
        {
            return Context.Set<TEntity>().AsQueryable();
        }

        public TEntity GetById(int id)
        {
            var result = Context.Set<TEntity>().Find(id);

            return result;
        }

        public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var result = await Context.Set<TEntity>().FindAsync(id, cancellationToken);

            return result;
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var result = await Context.AddAsync(entity, cancellationToken);

            return result.Entity;
        }

        public async Task AddRangeAsync(IList<TEntity> entities, CancellationToken cancellationToken)
        {
            await Context.AddRangeAsync(entities);
        }

        public void Delete(int id)
        {
            Context.Remove(id);
        }

        public void DeleteRange(IList<int> ids)
        {
            Context.RemoveRange(ids);
        }

        public void LogicalDelete(int id)
        {
            var entity = this.GetById(id);
            entity.IsDelete = true;
            this.Update(entity);
        }

        public void Update(TEntity entity)
        {
            entity.UpdateDate = DateTime.Now;
            Context.Update(entity);
        }
    }
}
