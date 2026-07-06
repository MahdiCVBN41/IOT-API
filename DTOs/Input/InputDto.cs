using EquipmentManagement.API.Models.Enums;

namespace EquipmentManagement.API.DTOs.Input;

public class InputDto
{
    public int Id { get; set; }
    public int EquipmentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string UnitOfMeasure { get; set; } = string.Empty;
    public InputType Type { get; set; }
}