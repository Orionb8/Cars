using System;
using System.Linq.Expressions;

namespace TestProject.Interfaces {
    public interface IViewModel<TEntity> : IEquatable<TEntity>
        where TEntity : class {
        bool IsNew();
        Expression<Func<TEntity, bool>> EqualsExpression { get; }
    }
}
