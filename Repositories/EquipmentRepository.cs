using Dapper;
using EquipmentManagement.API.Data;
using EquipmentManagement.API.Models;
using System.Data;

namespace EquipmentManagement.API.Repositories;

public class EquipmentRepository : IEquipmentRepository
{
    private readonly DbConnectionFactory _dbFactory;

    public EquipmentRepository(DbConnectionFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<IEnumerable<Equipment>> GetAllAsync()
    {
        using var connection = _dbFactory.CreateConnection();
        const string sql = "SELECT Id, Name, Location FROM Equipment";
        return await connection.QueryAsync<Equipment>(sql);
    }

    public async Task<Equipment?> GetByIdAsync(int id)
    {
        using var connection = _dbFactory.CreateConnection();
        const string sql = "SELECT Id, Name, Location FROM Equipment WHERE Id = @Id";
        return await connection.QuerySingleOrDefaultAsync<Equipment>(sql, new { Id = id });
    }

    public async Task<int> CreateAsync(Equipment equipment)
    {
        using var connection = _dbFactory.CreateConnection();
        const string sql = @"
            INSERT INTO Equipment (Name, Location)
            VALUES (@Name, @Location);
            SELECT CAST(SCOPE_IDENTITY() AS INT)";
        return await connection.ExecuteScalarAsync<int>(sql, equipment);
    }

    public async Task<bool> UpdateAsync(Equipment equipment)
    {
        using var connection = _dbFactory.CreateConnection();
        const string sql = @"
            UPDATE Equipment
            SET Name = @Name, Location = @Location
            WHERE Id = @Id";
        var rowsAffected = await connection.ExecuteAsync(sql, equipment);
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _dbFactory.CreateConnection();
        const string sql = "DELETE FROM Equipment WHERE Id = @Id";
        var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
        return rowsAffected > 0;
    }
 
    public async Task<bool> ExistsAsync(int id)
    {
        using var connection = _dbFactory.CreateConnection();
        const string sql = "SELECT COUNT(1) FROM Equipment WHERE Id = @Id";
        return await connection.ExecuteScalarAsync<bool>(sql, new { Id = id });
    }
}