using EquipmentManagement.API.DTOs.Equipment;

namespace EquipmentManagement.API.Services;

public interface IEquipmentService
{
    Task<IEnumerable<EquipmentDto>> GetAllAsync();
    Task<EquipmentDto?> GetByIdAsync(int id);
    Task<EquipmentDto> CreateAsync(CreateEquipmentDto dto);
    Task<EquipmentDto?> UpdateAsync(int id, UpdateEquipmentDto dto);
    Task<bool> DeleteAsync(int id);
}