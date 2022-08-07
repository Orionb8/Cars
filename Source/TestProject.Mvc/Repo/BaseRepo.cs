using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestProject.Interfaces;

namespace TestProject.Mvc.Repo {
    public class BaseRepo<TEntity> : IRepo<TEntity> where TEntity : class, new() {

        protected DbContext _ctx;

        public BaseRepo(DbContext ctx) {
            _ctx = ctx;
        }

        public async Task<ITransaction> BeginTransactionAsync(CancellationToken cancellationToken = default) {
            return new Transaction(await _ctx.Database.BeginTransactionAsync(cancellationToken));
        }
        public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default) {
            await _ctx.Set<TEntity>().AddAsync(entity);
            await SaveAsync();
            return entity;
        }
        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default) {
            _ctx.Set<TEntity>().Update(entity);
            await SaveAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default) {
            _ctx.Set<TEntity>().Remove(entity);
            return await SaveAsync();
        }

        public void Dispose() {
        }

        public async Task<bool> SaveAsync(CancellationToken cancellationToken = default) {
            return await _ctx.SaveChangesAsync() > 0;
        }

        public IQueryable<TEntity> Set() {
            return _ctx.Set<TEntity>();
        }

        public async Task<TEntity> GetSingle(int? id) {
            return await _ctx.Set<TEntity>().FindAsync(id);
        }
    }
}
