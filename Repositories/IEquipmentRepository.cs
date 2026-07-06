using EquipmentManagement.API.Models;

namespace EquipmentManagement.API.Repositories;

public interface IEquipmentRepository
{
    Task<IEnumerable<Equipment>> GetAllAsync();
    Task<Equipment?> GetByIdAsync(int id);
    Task<int> CreateAsync(Equipment equipment);
    Task<bool> UpdateAsync(Equipment equipment);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}