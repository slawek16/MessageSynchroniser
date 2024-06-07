namespace MessageSynchroniser.Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task<T> GetAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
    }
}
