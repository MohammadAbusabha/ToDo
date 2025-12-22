using System.Linq.Expressions;
using ToDo.Infrastructure.Interfaces;

namespace ToDo.Infrastructure.BaseSpecifications
{
    public class BaseSpecifications<T> : IBaseSpecification<T> where T : class // no need for third class / should just inject interface when needed
    {
        //i think we put the rule here
        public Expression<Func<T, bool>> Criteria { get; set; }
        public Expression<Func<T, bool>> Include { get; set; }
        //protected BaseSpecifications(Expression<Func<T, bool>> criteria, Expression<Func<T, bool>> include)
        //protected BaseSpecifications(Expression<Func<T, bool>> criteria)
        //{
        //    Criteria = criteria;
        //    //Include = include;
        //}
        public void SetCriteria(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public void SetInclude(Expression<Func<T, bool>> include)
        {
            Include = include;
        }
    }
}
