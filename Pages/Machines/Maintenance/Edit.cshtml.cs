using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TorqIQ.Data;
using TorqIQ.Models;

namespace TorqIQ.Pages.Machines.Maintenance;

public class EditModel : PageModel
{
    private readonly AppDbContext _db;
    public EditModel(AppDbContext db) => _db = db;

    public int MachineId { get; set; }
    public string MachineName { get; set; } = "";

    [BindProperty]
    public MaintenanceRecord Record { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int machineId, int id)
    {
        var record = await _db.MaintenanceRecords.FindAsync(id);
        if (record is null) return NotFound();
        var machine = await _db.Machines.FindAsync(machineId);
        MachineId = machineId;
        MachineName = machine?.Name ?? "";
        Record = record;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int machineId)
    {
        var existing = await _db.MaintenanceRecords.FindAsync(Record.Id);
        if (existing is null) return NotFound();
        existing.Date = Record.Date;
        existing.PerformedBy = Record.PerformedBy;
        existing.WorkDone = Record.WorkDone;
        existing.Result = Record.Result;
        await _db.SaveChangesAsync();
        return RedirectToPage("/Machines/Details", new { id = machineId, tab = "maint" });
    }
}
