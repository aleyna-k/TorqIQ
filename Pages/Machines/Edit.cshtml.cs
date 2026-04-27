using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TorqIQ.Data;
using TorqIQ.Models;

namespace TorqIQ.Pages.Machines;

public class EditModel : PageModel
{
    private readonly AppDbContext _db;
    public EditModel(AppDbContext db) => _db = db;

    [BindProperty]
    public Machine Machine { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var machine = await _db.Machines.FindAsync(id);
        if (machine is null) return NotFound();
        Machine = machine;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        var existing = await _db.Machines.FindAsync(Machine.Id);
        if (existing is null) return NotFound();

        existing.Code     = Machine.Code;
        existing.Name     = Machine.Name;
        existing.Brand    = Machine.Brand;
        existing.Period   = Machine.Period;
        existing.PlanDate = Machine.PlanDate;
        existing.CardDate = Machine.CardDate;
        existing.Energy   = Machine.Energy;
        existing.Voltage  = Machine.Voltage;
        existing.Pressure = Machine.Pressure;
        existing.Power    = Machine.Power;
        existing.Usage    = Machine.Usage;
        existing.Specs    = Machine.Specs;
        existing.Notes    = Machine.Notes;
        existing.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return RedirectToPage("Details", new { id = Machine.Id });
    }
}
