using Microsoft.EntityFrameworkCore.Storage;

namespace EducationalCourse.Domain.ICommandRepositories.Base
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task TransactionCommit(IDbContextTransaction transaction);

        Task<IDbContextTransaction> BeginTransactionAsync();

        Task TransactionRollback(IDbContextTransaction transaction);
    }
}
