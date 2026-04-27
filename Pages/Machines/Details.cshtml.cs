using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TorqIQ.Data;
using TorqIQ.Models;

namespace TorqIQ.Pages.Machines;

public class DetailsModel : PageModel
{
    private readonly AppDbContext _db;
    public DetailsModel(AppDbContext db) => _db = db;

    public Machine Machine { get; set; } = null!;
    public string ActiveTab { get; set; } = "info";

    public async Task<IActionResult> OnGetAsync(int id, string tab = "info")
    {
        var machine = await _db.Machines
            .Include(m => m.MaintenanceRecords)
            .Include(m => m.FailureRecords)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (machine is null) return NotFound();
        Machine = machine;
        ActiveTab = tab;
        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var machine = await _db.Machines.FindAsync(id);
        if (machine is not null)
        {
            _db.Machines.Remove(machine);
            await _db.SaveChangesAsync();
        }
        return RedirectToPage("/Index");
    }

    public async Task<IActionResult> OnPostDeleteMaintenanceAsync(int id, int recordId)
    {
        var record = await _db.MaintenanceRecords.FindAsync(recordId);
        if (record is not null)
        {
            _db.MaintenanceRecords.Remove(record);
            await _db.SaveChangesAsync();
        }
        return RedirectToPage(new { id, tab = "maint" });
    }

    public async Task<IActionResult> OnPostDeleteFailureAsync(int id, int recordId)
    {
        var record = await _db.FailureRecords.FindAsync(recordId);
        if (record is not null)
        {
            _db.FailureRecords.Remove(record);
            await _db.SaveChangesAsync();
        }
        return RedirectToPage(new { id, tab = "fail" });
    }
}
