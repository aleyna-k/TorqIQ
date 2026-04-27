using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TorqIQ.Data;
using TorqIQ.Models;

namespace TorqIQ.Pages.Machines.Failures;

public class EditModel : PageModel
{
    private readonly AppDbContext _db;
    public EditModel(AppDbContext db) => _db = db;

    public int MachineId { get; set; }
    public string MachineName { get; set; } = "";

    [BindProperty]
    public FailureRecord Record { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int machineId, int id)
    {
        var record = await _db.FailureRecords.FindAsync(id);
        if (record is null) return NotFound();
        var machine = await _db.Machines.FindAsync(machineId);
        MachineId = machineId;
        MachineName = machine?.Name ?? "";
        Record = record;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int machineId)
    {
        var existing = await _db.FailureRecords.FindAsync(Record.Id);
        if (existing is null) return NotFound();
        existing.Date        = Record.Date;
        existing.Description = Record.Description;
        existing.FaultType   = Record.FaultType;
        existing.Action1     = Record.Action1;
        existing.Action2     = Record.Action2;
        existing.AssignedTo  = Record.AssignedTo;
        existing.Result      = Record.Result;
        await _db.SaveChangesAsync();
        return RedirectToPage("/Machines/Details", new { id = machineId, tab = "fail" });
    }
}
