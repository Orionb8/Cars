using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TestProject.Interfaces {
    public interface IRepo<TEntity>
        : IDisposable
        where TEntity : class {
        IQueryable<TEntity> Set();
        Task<TEntity> GetSingle(int? id);
        Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<ITransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task<bool> SaveAsync(CancellationToken cancellationToken = default);
    }
}
