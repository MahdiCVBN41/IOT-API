namespace EquipmentManagement.API.DTOs.ValueLog;

public class ValueLogDto
{
    public int Id { get; set; }
    public int InputId { get; set; }
    public DateOnly LogDate { get; set; }
    public TimeOnly LogTime { get; set; }
    public decimal Value { get; set; }
}