using System.Linq.Expressions;

namespace ToDo.Core.SpecTest
{
    public interface ISpecification<T> where T : class
    {
        Expression<Func<T, bool>> Criteria { get; set; }
        List<Expression<Func<T, object>>>  Include { get; set; }
    }
}
