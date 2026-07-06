using EquipmentManagement.API.DTOs.Input;
using EquipmentManagement.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InputController : ControllerBase
{
    private readonly IInputService _service;

    public InputController(IInputService service)
    {
        _service = service;
    }

    [HttpGet("by-equipment/{equipmentId}")]
    public async Task<IActionResult> GetByEquipmentId(int equipmentId)
    {
        try
        {
            var items = await _service.GetByEquipmentIdAsync(equipmentId);
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
    public async Task<IActionResult> Create([FromBody] CreateInputDto dto)
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
    public async Task<IActionResult> Update(int id, [FromBody] UpdateInputDto dto)
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