using EducationalCourse.DataAccess.EF.Context;
using EducationalCourse.Domain.ICommandRepositories.Base;
using Microsoft.EntityFrameworkCore.Storage;

namespace Sample.DataAccess.EF.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Constractor

        public UnitOfWork(EducationalCourseContext context)
        {
            Context = context;
        }

        private EducationalCourseContext Context { get; }
        private bool _disposed = false;

        #endregion Constractor

        #region Transaction

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await Context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
        {
            return await Context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task TransactionCommit(IDbContextTransaction transaction)
        {
            await transaction.CommitAsync();
        }

        public async Task TransactionRollback(IDbContextTransaction transaction)
        {
            await transaction.RollbackAsync();
        }

        #endregion Transaction

        #region Dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion Dispose
    }
}
