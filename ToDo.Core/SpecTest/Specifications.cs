using AutoMapper.Execution;
using System.Linq.Expressions;

namespace ToDo.Core.SpecTest
{
    public class Specifications<T> : ISpecification<T>
    {
        public Specifications(Expression<Func<T, bool>> criteria = null)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Include { get; } = new List<Expression<Func<T, object>>>();
        public ISpecification<T> AddInclude(Expression<Func<T, object>> expression)
        {
            Include.Add(expression);
            return this;
        }
        public ISpecification<T> AddCriteria(Expression<Func<T, bool>> criteria)// always returns null
        {
            return this;
        }
    }
}
