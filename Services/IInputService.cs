using EquipmentManagement.API.DTOs.Input;

namespace EquipmentManagement.API.Services;

public interface IInputService
{
    Task<IEnumerable<InputDto>> GetByEquipmentIdAsync(int equipmentId);
    Task<InputDto?> GetByIdAsync(int id);
    Task<InputDto> CreateAsync(CreateInputDto dto);
    Task<InputDto?> UpdateAsync(int id, UpdateInputDto dto);
    Task<bool> DeleteAsync(int id);
}