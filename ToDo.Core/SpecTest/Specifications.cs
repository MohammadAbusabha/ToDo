using System.Linq.Expressions;

namespace ToDo.Core.SpecTest
{
    public class Specifications<T> : ISpecification<T> where T : class
    {
        public Specifications(Expression<Func<T, bool>> criteria = null)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Include { get; set; }
        public void AddInclude(Expression<Func<T, object>> expression)
        {
            Include.Add(expression);
        }
    }
}
