using System.ComponentModel.DataAnnotations;

namespace TorqIQ.Models;

public class MaintenanceRecord
{
    public int Id { get; set; }
    public int MachineId { get; set; }
    public Machine Machine { get; set; } = null!;

    [MaxLength(50)]
    public string? Date { get; set; }

    [MaxLength(100)]
    public string? PerformedBy { get; set; }

    public string? WorkDone { get; set; }

    [MaxLength(50)]
    public string Result { get; set; } = "OK";
}
