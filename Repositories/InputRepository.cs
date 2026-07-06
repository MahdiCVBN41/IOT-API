using Dapper;
using EquipmentManagement.API.Data;
using EquipmentManagement.API.Models;
using EquipmentManagement.API.Models.Enums;

namespace EquipmentManagement.API.Repositories;

public class InputRepository : IInputRepository
{
    private readonly DbConnectionFactory _dbFactory;

    public InputRepository(DbConnectionFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<IEnumerable<Input>> GetByEquipmentIdAsync(int equipmentId)
    {
        using var connection = _dbFactory.CreateConnection();
        const string sql = @"
            SELECT Id, EquipmentId, Name, UnitOfMeasure, Type
            FROM Input
            WHERE EquipmentId = @EquipmentId";
        var inputs = await connection.QueryAsync<Input, string, Input>(
            sql,
            (input, typeStr) =>
            {
                input.Type = Enum.Parse<InputType>(typeStr);
                return input;
            },
            new { EquipmentId = equipmentId },
            splitOn: "Type"
        );
        return inputs;
    }

    public async Task<Input?> GetByIdAsync(int id)
    {
        using var connection = _dbFactory.CreateConnection();
        const string sql = @"
            SELECT Id, EquipmentId, Name, UnitOfMeasure, Type
            FROM Input
            WHERE Id = @Id";
        return await connection.QuerySingleOrDefaultAsync<Input>(sql, new { Id = id });
    }

    public async Task<int> CreateAsync(Input input)
    {
        using var connection = _dbFactory.CreateConnection();
        const string sql = @"
            INSERT INTO Input (EquipmentId, Name, UnitOfMeasure, Type)
            VALUES (@EquipmentId, @Name, @UnitOfMeasure, @Type);
            SELECT CAST(SCOPE_IDENTITY() AS INT)";
        return await connection.ExecuteScalarAsync<int>(sql, new
        {
            input.EquipmentId,
            input.Name,
            input.UnitOfMeasure,
            Type = input.Type.ToString()
        });
    }

    public async Task<bool> UpdateAsync(Input input)
    {
        using var connection = _dbFactory.CreateConnection();
        const string sql = @"
            UPDATE Input
            SET Name = @Name, UnitOfMeasure = @UnitOfMeasure, Type = @Type
            WHERE Id = @Id";
        var rowsAffected = await connection.ExecuteAsync(sql, new
        {
            input.Name,
            input.UnitOfMeasure,
            Type = input.Type.ToString(),
            input.Id
        });
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _dbFactory.CreateConnection();
        const string sql = "DELETE FROM Input WHERE Id = @Id";
        var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
        return rowsAffected > 0;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        using var connection = _dbFactory.CreateConnection();
        const string sql = "SELECT COUNT(1) FROM Input WHERE Id = @Id";
        return await connection.ExecuteScalarAsync<bool>(sql, new { Id = id });
    }
}