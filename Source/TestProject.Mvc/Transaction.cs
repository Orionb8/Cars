using Microsoft.EntityFrameworkCore.Storage;
using TestProject.Interfaces;

namespace TestProject.Mvc {
    internal class Transaction : ITransaction {
        IDbContextTransaction _transaction;
        public Transaction(IDbContextTransaction transaction) {
            _transaction = transaction;
        }

        public void Commit() {
            _transaction.Commit();
        }

        public void Rollback() {
            _transaction.Rollback();
        }

        public void Dispose() {
            _transaction.Dispose();
        }
    }
}
