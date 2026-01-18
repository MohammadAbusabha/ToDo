using System.Linq.Expressions;

namespace ToDo.Core.SpecTest
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Include { get; }
        ISpecification<T> AddCriteria(Expression<Func<T, bool>> criteria);
        ISpecification<T> AddInclude(Expression<Func<T, object>> expression);
    }
}
