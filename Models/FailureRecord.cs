using System.ComponentModel.DataAnnotations;

namespace TorqIQ.Models;

public class FailureRecord
{
    public int Id { get; set; }
    public int MachineId { get; set; }
    public Machine Machine { get; set; } = null!;

    [MaxLength(50)]
    public string? Date { get; set; }

    [MaxLength(200)]
    public string? Description { get; set; }

    [MaxLength(50)]
    public string? FaultType { get; set; }

    public string? Action1 { get; set; }
    public string? Action2 { get; set; }

    [MaxLength(100)]
    public string? AssignedTo { get; set; }

    [MaxLength(50)]
    public string Result { get; set; } = "OK";
}
