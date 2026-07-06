using EquipmentManagement.API.Models;

namespace EquipmentManagement.API.Repositories;

public interface IInputRepository
{
    Task<IEnumerable<Input>> GetByEquipmentIdAsync(int equipmentId);
    Task<Input?> GetByIdAsync(int id);
    Task<int> CreateAsync(Input input);
    Task<bool> UpdateAsync(Input input);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}