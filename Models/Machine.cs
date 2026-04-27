using System.ComponentModel.DataAnnotations;

namespace TorqIQ.Models;

public class Machine
{
    public int Id { get; set; }

    [Required, MaxLength(20)]
    public string Code { get; set; } = "";

    [Required, MaxLength(100)]
    public string Name { get; set; } = "";

    [MaxLength(100)]
    public string? Brand { get; set; }

    [MaxLength(50)]
    public string Period { get; set; } = "Annually";

    public DateOnly? PlanDate { get; set; }
    public DateOnly? CardDate { get; set; }

    [MaxLength(50)]
    public string Energy { get; set; } = "Electric";

    [MaxLength(30)]
    public string? Voltage { get; set; }

    [MaxLength(30)]
    public string? Pressure { get; set; }

    [MaxLength(30)]
    public string? Power { get; set; }

    [MaxLength(100)]
    public string? Usage { get; set; }

    public string? Specs { get; set; }
    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<MaintenanceRecord> MaintenanceRecords { get; set; } = new List<MaintenanceRecord>();
    public ICollection<FailureRecord> FailureRecords { get; set; } = new List<FailureRecord>();
}
