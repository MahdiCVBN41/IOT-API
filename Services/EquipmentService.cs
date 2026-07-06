using EquipmentManagement.API.DTOs.Equipment;
using EquipmentManagement.API.Models;
using EquipmentManagement.API.Repositories;

namespace EquipmentManagement.API.Services;

public class EquipmentService : IEquipmentService
{
    private readonly IEquipmentRepository _repository;

    public EquipmentService(IEquipmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<EquipmentDto>> GetAllAsync()
    {
        var equipments = await _repository.GetAllAsync();
        return equipments.Select(e => new EquipmentDto
        {
            Id = e.Id,
            Name = e.Name,
            Location = e.Location
        });
    }

    public async Task<EquipmentDto?> GetByIdAsync(int id)
    {
        var equipment = await _repository.GetByIdAsync(id);
        if (equipment == null) return null;
        return new EquipmentDto
        {
            Id = equipment.Id,
            Name = equipment.Name,
            Location = equipment.Location
        };
    }

    public async Task<EquipmentDto> CreateAsync(CreateEquipmentDto dto)
    {
        var equipment = new Equipment
        {
            Name = dto.Name,
            Location = dto.Location
        };
        var id = await _repository.CreateAsync(equipment);
        return new EquipmentDto
        {
            Id = id,
            Name = equipment.Name,
            Location = equipment.Location
        };
    }

    public async Task<EquipmentDto?> UpdateAsync(int id, UpdateEquipmentDto dto)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null) return null;

        existing.Name = dto.Name;
        existing.Location = dto.Location;

        var updated = await _repository.UpdateAsync(existing);
        if (!updated) return null;

        return new EquipmentDto
        {
            Id = existing.Id,
            Name = existing.Name,
            Location = existing.Location
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}