using EquipmentManagement.API.DTOs.ValueLog;

namespace EquipmentManagement.API.Services;

public interface IValueLogService
{
    Task<IEnumerable<ValueLogDto>> GetByInputIdAsync(int inputId);
    Task<ValueLogDto?> GetByIdAsync(int id);
    Task<ValueLogDto> CreateAsync(CreateValueLogDto dto);
    Task<ValueLogDto?> UpdateAsync(int id, CreateValueLogDto dto); // using same DTO for simplicity
    Task<bool> DeleteAsync(int id);
}