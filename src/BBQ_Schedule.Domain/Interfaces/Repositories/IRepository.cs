namespace BBQ_Schedule.Domain.Interfaces.Repositories
{
    public interface IRepository<T>: IDisposable where T : IAggregationRoot
    {
        public IUnitOfWork UnitOfWork { get; }
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(Guid id);
    }
}
