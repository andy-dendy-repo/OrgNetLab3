using Dapper;
using System.Data;

namespace OrgNetLab3.Data.Core
{
    public abstract class BaseRepository<T> : IBaseRepository<T>
    {
        protected readonly IDbConnection _connection;
        public string UserId { get; set; }

        private const string _logPath = "log.txt";
        public BaseRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        protected async Task Log(string sql)
        {
            try
            {
                await File.AppendAllTextAsync(_logPath, $"SQL: [{sql}]; USER: [{UserId}]\n");
            }
            catch
            {

            }
        }

        protected abstract void SetPrimaryKeysBeforeInsert(T entity);
        protected virtual object GetKeysContainer(object[] keys)
        {
            if (keys == null || keys.Length == 0)
                return new { Id = (string)null };
            else
                return new { Id = keys[0] };
        }

        public virtual async Task Create(T entity)
        {
            SetPrimaryKeysBeforeInsert(entity);
            var procedure = $"[Insert {typeof(T).Name}]";
            await Log(procedure);
            await _connection.ExecuteAsync(procedure, entity, commandType: CommandType.StoredProcedure);
        }

        public virtual async Task Update(T entity)
        {
            var procedure = $"[Update {typeof(T).Name}]";
            await Log(procedure);
            await _connection.ExecuteAsync(procedure, entity, commandType: CommandType.StoredProcedure);
        }

        public virtual async Task<int> Delete(params object[] keys)
        {
            var procedure = $"[Delete {typeof(T).Name}]";
            await Log(procedure);
            var id = await _connection.ExecuteAsync(procedure, GetKeysContainer(keys), commandType: CommandType.StoredProcedure);
            return id;
        }

        public virtual async Task<IList<T>> Get()
        {
            var procedure = $"[Select {typeof(T).Name}]";
            await Log(procedure);
            var enumeration = await _connection.QueryAsync<T>(procedure, GetKeysContainer(null), commandType: CommandType.StoredProcedure);
            return enumeration.ToList();
        }

        public virtual async Task<T> GetById(params object[] keys)
        {
            var procedure = $"[Select {typeof(T).Name}]";
            await Log(procedure);
            var item = await _connection.QueryFirstOrDefaultAsync<T>(procedure, GetKeysContainer(keys), commandType: CommandType.StoredProcedure);
            return item;
        }
    }
}
