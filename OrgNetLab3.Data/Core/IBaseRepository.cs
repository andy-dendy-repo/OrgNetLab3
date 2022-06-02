namespace OrgNetLab3.Data.Core
{
    public interface IBaseRepository<T>
    {
        public Task Create(T entity);

        public Task<T> GetById(params object[] keys);

        public Task<IList<T>> Get();

        public Task<int> Delete(params object[] keys);

        public Task Update(T entity);

        public string UserId { get; set; }
    }
}
