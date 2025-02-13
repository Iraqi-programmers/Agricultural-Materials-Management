using Microsoft.Data.SqlClient;
using MyLib.MyLibrary;
using System.Data;

namespace MyLib
{

    public partial class CRUD
    {
        public static async Task<object?> GetByColumnValueAsync(string query, string columnName, object columnValue, CommandType type = CommandType.Text, byte retryAttempts = 5, ushort retryDelayMilliseconds = 500)
        {
            SqlParameter[] parameters = { new SqlParameter($"@{columnName}", columnValue) };
            return (await _ExecuteReaderAsync(query, parameters, type, retryAttempts, retryDelayMilliseconds)).FirstOrDefault();
        }

        public static async Task<T> GetByColumnValueAsync<T>(string query, string columnName, object columnValue, CommandType type = CommandType.Text, byte retryAttempts = 5, ushort retryDelayMilliseconds = 500)
        {
            SqlParameter[] parameters = { new SqlParameter($"@{columnName}", columnValue) };
            return (await _ExecuteReaderAsync<T>(query, parameters, type, retryAttempts, retryDelayMilliseconds)).FirstOrDefault();
        }

        public static async Task<List<object[]>> GetAllAsListAsync(string query, SqlParameter[]? parameters = null, CommandType type = CommandType.Text, byte retryAttempts = 5, ushort retryDelayMilliseconds = 500)
            => await _ExecuteReaderAsync(query, parameters, type, retryAttempts, retryDelayMilliseconds);

        public static async Task<List<T>> GetAllAsListAsync<T>(string query, SqlParameter[]? parameters = null, CommandType type = CommandType.Text, byte retryAttempts = 5, ushort retryDelayMilliseconds = 500)
            => await _ExecuteReaderAsync<T>(query, parameters, type, retryAttempts, retryDelayMilliseconds);

        public static async Task<DataTable> GetAllAsDataTableAsync(string query, SqlParameter[]? parameters = null, CommandType type = CommandType.Text, byte retryAttempts = 5, ushort retryDelayMilliseconds = 500)
            => await _ExecuteDataAdapterAsync(query, parameters, type, retryAttempts, retryDelayMilliseconds);

        public static async Task<int?> AddAsync(string query, SqlParameter[]? parameters = null, CommandType type = CommandType.Text, byte retryAttempts = 5, ushort retryDelayMilliseconds = 500)
        {
            object result = await _ExecuteScalarAsync(query, parameters, type, retryAttempts, retryDelayMilliseconds);
            return result != null ? Convert.ToInt32(result) : (int?)Convert.DBNull;
        }

        public static async Task<bool> UpdateAsync(string query, SqlParameter[]? parameters = null, CommandType type = CommandType.Text, byte retryAttempts = 5, ushort retryDelayMilliseconds = 500)
        {
            var result = await _ExecuteNonQueryAsync(query, parameters, type, retryAttempts, retryDelayMilliseconds);
            return result != null && result > 0;
        }

        public static async Task<bool> DeleteAsync(string query, SqlParameter[]? parameters = null, CommandType type = CommandType.Text, byte retryAttempts = 5, ushort retryDelayMilliseconds = 500)
        {
            var result = await _ExecuteNonQueryAsync(query, parameters, type, retryAttempts, retryDelayMilliseconds);
            return result != null && result > 0;
        }

        public static async Task<bool> IsExistAsync(string query, SqlParameter[]? parameters = null, CommandType type = CommandType.Text, byte retryAttempts = 5, ushort retryDelayMilliseconds = 500)
            => await _ExecuteScalarAsync(query, parameters, type, retryAttempts, retryDelayMilliseconds) != null;

        public static async Task<bool> ExecuteBatchOperationsAsync(List<(string query, SqlParameter[] parameters, CommandType type)> commands)
            => await _ExecuteTransactionAsync(commands);
    }

    public partial class CRUD : clsDatabaseExecutor
    {
        private CRUD() { }

        public static object? GetByColumnValue(string query, string columnName, object columnValue, CommandType type = CommandType.Text, byte retryAttempts = 5, ushort retryDelayMilliseconds = 500)
        {
            SqlParameter[] parameters = { new SqlParameter($"@{columnName}", columnValue) };
            return _ExecuteReader(query, parameters, type, retryAttempts, retryDelayMilliseconds).FirstOrDefault();
        }

        public static T? GetByColumnValue<T>(string query, string columnName, object columnValue, CommandType type = CommandType.Text, byte retryAttempts = 5, ushort retryDelayMilliseconds = 500)
        {
            SqlParameter[] parameters = { new SqlParameter($"@{columnName}", columnValue) };
            return _ExecuteReader<T>(query, parameters, type, retryAttempts, retryDelayMilliseconds).FirstOrDefault();
        }

        public static List<object[]>? GetAllAsList(string query, SqlParameter[]? parameters = null, CommandType type = CommandType.Text, byte retryAttempts = 5, ushort retryDelayMilliseconds = 500)
            => _ExecuteReader(query, parameters, type, retryAttempts, retryDelayMilliseconds);

        public static List<T>? GetAllAsList<T>(string query, SqlParameter[]? parameters = null, CommandType type = CommandType.Text, byte retryAttempts = 5, ushort retryDelayMilliseconds = 500)
            => _ExecuteReader<T>(query, parameters, type, retryAttempts, retryDelayMilliseconds);

        public static DataTable GetAllAsDataTable(string query, SqlParameter[]? parameters = null, CommandType type = CommandType.Text, byte retryAttempts = 5, ushort retryDelayMilliseconds = 500)
            => _ExecuteDataAdapter(query, parameters, type, retryAttempts, retryDelayMilliseconds);

        public static int? Add(string query, SqlParameter[]? parameters = null, CommandType type = CommandType.Text, byte retryAttempts = 5, ushort retryDelayMilliseconds = 500)
        {
            object? result = _ExecuteScalar(query, parameters, type, retryAttempts, retryDelayMilliseconds);
            return result != null ? Convert.ToInt32(result) : (int?)Convert.DBNull;
        }

        public static bool Update(string query, SqlParameter[]? parameters = null, CommandType type = CommandType.Text, byte retryAttempts = 5, ushort retryDelayMilliseconds = 500)
        {
            var result = _ExecuteNonQuery(query, parameters, type, retryAttempts, retryDelayMilliseconds);
            return result != null && result > 0;
        }

        public static bool Delete(string query, SqlParameter[]? parameters = null, CommandType type = CommandType.Text, byte retryAttempts = 5, ushort retryDelayMilliseconds = 500)
        {
            var result = _ExecuteNonQuery(query, parameters, type, retryAttempts, retryDelayMilliseconds);
            return result != null && result > 0;
        }

        public static bool IsExist(string query, SqlParameter[]? parameters = null, CommandType type = CommandType.Text, byte retryAttempts = 5, ushort retryDelayMilliseconds = 500)
            => _ExecuteScalar(query, parameters, type, retryAttempts, retryDelayMilliseconds) != null;

        public static bool ExecuteBatchOperations(List<(string query, SqlParameter[] parameters, CommandType type)> commands)
            => _ExecuteTransaction(commands);
    }

}
