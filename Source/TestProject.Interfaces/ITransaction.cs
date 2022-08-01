using System;

namespace TestProject.Interfaces {
    public interface ITransaction : IDisposable {
        void Commit();
        void Rollback();
    }
}
