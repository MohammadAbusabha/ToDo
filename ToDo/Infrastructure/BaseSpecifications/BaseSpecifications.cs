using System;
using System.Linq.Expressions;
using ToDo.Infrastructure.Interfaces;

namespace ToDo.Infrastructure.BaseSpecifications
{
    public class BaseSpecifications<T> : IBaseSpecification<T> where T : class
    {
        //i think we put the rule here

        public Expression<Func<T, bool>> Criteria { get; }
        public Expression<Func<T, bool>> Include { get; }
        //protected BaseSpecifications(Expression<Func<T, bool>> criteria, Expression<Func<T, bool>> include)
        protected BaseSpecifications(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
            //Include = include;
        }
    }
}
