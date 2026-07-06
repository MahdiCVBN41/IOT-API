using Dapper;
using EquipmentManagement.API.Data;
using EquipmentManagement.API.Models;

namespace EquipmentManagement.API.Repositories;

public class ValueLogRepository : IValueLogRepository
{
    private readonly DbConnectionFactory _dbFactory;

    public ValueLogRepository(DbConnectionFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<IEnumerable<ValueLog>> GetByInputIdAsync(int inputId)
    {
        using var connection = _dbFactory.CreateConnection();
        const string sql = @"
            SELECT Id, InputId, LogDate, LogTime, Value
            FROM ValueLog
            WHERE InputId = @InputId";
        return await connection.QueryAsync<ValueLog>(sql, new { InputId = inputId });
    }

    public async Task<ValueLog?> GetByIdAsync(int id)
    {
        using var connection = _dbFactory.CreateConnection();
        const string sql = @"
            SELECT Id, InputId, LogDate, LogTime, Value
            FROM ValueLog
            WHERE Id = @Id";
        return await connection.QuerySingleOrDefaultAsync<ValueLog>(sql, new { Id = id });
    }

    public async Task<int> CreateAsync(ValueLog log)
    {
        using var connection = _dbFactory.CreateConnection();
        const string sql = @"
            INSERT INTO ValueLog (InputId, LogDate, LogTime, Value)
            VALUES (@InputId, @LogDate, @LogTime, @Value);
            SELECT CAST(SCOPE_IDENTITY() AS INT)";
        return await connection.ExecuteScalarAsync<int>(sql, log);
    }

    public async Task<bool> UpdateAsync(ValueLog log)
    {
        using var connection = _dbFactory.CreateConnection();
        const string sql = @"
            UPDATE ValueLog
            SET LogDate = @LogDate, LogTime = @LogTime, Value = @Value
            WHERE Id = @Id";
        var rowsAffected = await connection.ExecuteAsync(sql, log);
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _dbFactory.CreateConnection();
        const string sql = "DELETE FROM ValueLog WHERE Id = @Id";
        var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
        return rowsAffected > 0;
    }
}