using EquipmentManagement.API.Models;

namespace EquipmentManagement.API.Repositories;

public interface IValueLogRepository
{
    Task<IEnumerable<ValueLog>> GetByInputIdAsync(int inputId);
    Task<ValueLog?> GetByIdAsync(int id);
    Task<int> CreateAsync(ValueLog log);
    Task<bool> UpdateAsync(ValueLog log);
    Task<bool> DeleteAsync(int id);
}