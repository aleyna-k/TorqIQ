using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TorqIQ.Data;
using TorqIQ.Models;

namespace TorqIQ.Pages.Machines.Failures;

public class CreateModel : PageModel
{
    private readonly AppDbContext _db;
    public CreateModel(AppDbContext db) => _db = db;

    public int MachineId { get; set; }
    public string MachineName { get; set; } = "";

    [BindProperty]
    public FailureRecord Record { get; set; } = new() { Result = "OK" };

    public async Task<IActionResult> OnGetAsync(int machineId)
    {
        var machine = await _db.Machines.FindAsync(machineId);
        if (machine is null) return NotFound();
        MachineId = machineId;
        MachineName = machine.Name;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int machineId)
    {
        Record.MachineId = machineId;
        _db.FailureRecords.Add(Record);
        await _db.SaveChangesAsync();
        return RedirectToPage("/Machines/Details", new { id = machineId, tab = "fail" });
    }
}
