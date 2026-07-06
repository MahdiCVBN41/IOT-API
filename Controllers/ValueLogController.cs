using EquipmentManagement.API.DTOs.ValueLog;
using EquipmentManagement.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ValueLogController : ControllerBase
{
    private readonly IValueLogService _service;

    public ValueLogController(IValueLogService service)
    {
        _service = service;
    }

    [HttpGet("by-input/{inputId}")]
    public async Task<IActionResult> GetByInputId(int inputId)
    {
        try
        {
            var items = await _service.GetByInputIdAsync(inputId);
            return Ok(items);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateValueLogDto dto)
    {
        try
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateValueLogDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}