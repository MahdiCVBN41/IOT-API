using System.ComponentModel.DataAnnotations;
using EquipmentManagement.API.Models.Enums;

namespace EquipmentManagement.API.DTOs.Input;

public class UpdateInputDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string UnitOfMeasure { get; set; } = string.Empty;

    [Required]
    public InputType Type { get; set; }
}