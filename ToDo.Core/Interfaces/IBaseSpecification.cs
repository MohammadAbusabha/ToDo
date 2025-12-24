using System.Linq.Expressions;

namespace ToDo.Core.Interfaces
{
    public interface IBaseSpecification<T> where T : class
    {
        public Expression<Func<T, bool>> Criteria { get; }
        public Expression<Func<T, bool>> Include { get; }
    }
}
