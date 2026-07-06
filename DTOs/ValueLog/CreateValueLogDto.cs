using System.ComponentModel.DataAnnotations;

namespace EquipmentManagement.API.DTOs.ValueLog;

public class CreateValueLogDto
{
    [Required]
    public int InputId { get; set; }

    [Required]
    public DateOnly LogDate { get; set; }

    [Required]
    public TimeOnly LogTime { get; set; }

    [Required]
    public decimal Value { get; set; }
}