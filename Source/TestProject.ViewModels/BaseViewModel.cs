using System;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using TestProject.Interfaces;

namespace TestProject.ViewModels {
    public class BaseViewModel<TEntity> : IViewModel<TEntity> where TEntity : class {
        public int? Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public Expression<Func<TEntity, bool>> EqualsExpression => EqualsExpr();

        public bool Equals(TEntity other) {
            throw new NotImplementedException();
        }

        public bool IsNew() {
            return !(Id.HasValue && Id > 0);
        }

        private Expression<Func<TEntity, bool>> EqualsExpr() {
            if(IsNew()) return x => false;

            var param = Expression.Parameter(typeof(TEntity), "x");
            Expression exp = Expression.Property(param, "Id");
            exp = Expression.Equal(exp, Expression.Constant(Id));
            return Expression.Lambda<Func<TEntity, bool>>(exp, param);
        }
    }
}
