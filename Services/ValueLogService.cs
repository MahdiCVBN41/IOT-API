using EquipmentManagement.API.DTOs.ValueLog;
using EquipmentManagement.API.Models;
using EquipmentManagement.API.Repositories;

namespace EquipmentManagement.API.Services;

public class ValueLogService : IValueLogService
{
    private readonly IValueLogRepository _logRepository;
    private readonly IInputRepository _inputRepository;

    public ValueLogService(IValueLogRepository logRepository, IInputRepository inputRepository)
    {
        _logRepository = logRepository;
        _inputRepository = inputRepository;
    }

    public async Task<IEnumerable<ValueLogDto>> GetByInputIdAsync(int inputId)
    {
        if (!await _inputRepository.ExistsAsync(inputId))
            throw new KeyNotFoundException($"Input with ID {inputId} not found.");

        var logs = await _logRepository.GetByInputIdAsync(inputId);
        return logs.Select(l => new ValueLogDto
        {
            Id = l.Id,
            InputId = l.InputId,
            LogDate = l.LogDate,
            LogTime = l.LogTime,
            Value = l.Value
        });
    }

    public async Task<ValueLogDto?> GetByIdAsync(int id)
    {
        var log = await _logRepository.GetByIdAsync(id);
        if (log == null) return null;
        return new ValueLogDto
        {
            Id = log.Id,
            InputId = log.InputId,
            LogDate = log.LogDate,
            LogTime = log.LogTime,
            Value = log.Value
        };
    }

    public async Task<ValueLogDto> CreateAsync(CreateValueLogDto dto)
    {
        if (!await _inputRepository.ExistsAsync(dto.InputId))
            throw new KeyNotFoundException($"Input with ID {dto.InputId} not found.");

        var log = new ValueLog
        {
            InputId = dto.InputId,
            LogDate = dto.LogDate,
            LogTime = dto.LogTime,
            Value = dto.Value
        };
        var id = await _logRepository.CreateAsync(log);
        return new ValueLogDto
        {
            Id = id,
            InputId = log.InputId,
            LogDate = log.LogDate,
            LogTime = log.LogTime,
            Value = log.Value
        };
    }

    public async Task<ValueLogDto?> UpdateAsync(int id, CreateValueLogDto dto)
    {
        var existing = await _logRepository.GetByIdAsync(id);
        if (existing == null) return null;

        existing.LogDate = dto.LogDate;
        existing.LogTime = dto.LogTime;
        existing.Value = dto.Value;

        var updated = await _logRepository.UpdateAsync(existing);
        if (!updated) return null;

        return new ValueLogDto
        {
            Id = existing.Id,
            InputId = existing.InputId,
            LogDate = existing.LogDate,
            LogTime = existing.LogTime,
            Value = existing.Value
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _logRepository.DeleteAsync(id);
    }
}