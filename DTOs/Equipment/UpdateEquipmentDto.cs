using System.ComponentModel.DataAnnotations;

namespace EquipmentManagement.API.DTOs.Equipment;

public class UpdateEquipmentDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Location { get; set; } = string.Empty;
}