//using Microsoft.EntityFrameworkCore;

//namespace ToDo.Core.SpecTest
//{
//    public class SpecificationEvaluator<T> where T : class
//    {
//        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
//        {
//            var query = inputQuery;
//            if(spec.Criteria != null)
//            {
//                query = query.Where(spec.Criteria);
//            }
//            query = spec.Include.Aggregate(query, (current, include) => current.Include(include));
//            return query;
//        }
//    }
//}
