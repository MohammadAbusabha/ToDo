namespace ToDo.Core.Interfaces
{
    public interface IRepository<T> where T : class // one repo for full app
    {
        Task<T> GetAsync(IBaseSpecification<T> spec);
    }
}
