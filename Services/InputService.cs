using EquipmentManagement.API.DTOs.Input;
using EquipmentManagement.API.Models;
using EquipmentManagement.API.Repositories;

namespace EquipmentManagement.API.Services;

public class InputService : IInputService
{
    private readonly IInputRepository _inputRepository;
    private readonly IEquipmentRepository _equipmentRepository;

    public InputService(IInputRepository inputRepository, IEquipmentRepository equipmentRepository)
    {
        _inputRepository = inputRepository;
        _equipmentRepository = equipmentRepository;
    }

    public async Task<IEnumerable<InputDto>> GetByEquipmentIdAsync(int equipmentId)
    {
        if (!await _equipmentRepository.ExistsAsync(equipmentId))
            throw new KeyNotFoundException($"Equipment with ID {equipmentId} not found.");

        var inputs = await _inputRepository.GetByEquipmentIdAsync(equipmentId);
        return inputs.Select(i => new InputDto
        {
            Id = i.Id,
            EquipmentId = i.EquipmentId,
            Name = i.Name,
            UnitOfMeasure = i.UnitOfMeasure,
            Type = i.Type
        });
    }

    public async Task<InputDto?> GetByIdAsync(int id)
    {
        var input = await _inputRepository.GetByIdAsync(id);
        if (input == null) return null;
        return new InputDto
        {
            Id = input.Id,
            EquipmentId = input.EquipmentId,
            Name = input.Name,
            UnitOfMeasure = input.UnitOfMeasure,
            Type = input.Type
        };
    }

    public async Task<InputDto> CreateAsync(CreateInputDto dto)
    {
        if (!await _equipmentRepository.ExistsAsync(dto.EquipmentId))
            throw new KeyNotFoundException($"Equipment with ID {dto.EquipmentId} not found.");

        var input = new Input
        {
            EquipmentId = dto.EquipmentId,
            Name = dto.Name,
            UnitOfMeasure = dto.UnitOfMeasure,
            Type = dto.Type
        };
        var id = await _inputRepository.CreateAsync(input);
        return new InputDto
        {
            Id = id,
            EquipmentId = input.EquipmentId,
            Name = input.Name,
            UnitOfMeasure = input.UnitOfMeasure,
            Type = input.Type
        };
    }

    public async Task<InputDto?> UpdateAsync(int id, UpdateInputDto dto)
    {
        var existing = await _inputRepository.GetByIdAsync(id);
        if (existing == null) return null;

        existing.Name = dto.Name;
        existing.UnitOfMeasure = dto.UnitOfMeasure;
        existing.Type = dto.Type;

        var updated = await _inputRepository.UpdateAsync(existing);
        if (!updated) return null;

        return new InputDto
        {
            Id = existing.Id,
            EquipmentId = existing.EquipmentId,
            Name = existing.Name,
            UnitOfMeasure = existing.UnitOfMeasure,
            Type = existing.Type
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _inputRepository.DeleteAsync(id);
    }
}