namespace EquipmentManagement.API.DTOs.Equipment;

public class EquipmentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
}