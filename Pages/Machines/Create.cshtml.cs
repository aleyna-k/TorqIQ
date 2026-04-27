using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TorqIQ.Data;
using TorqIQ.Models;

namespace TorqIQ.Pages.Machines;

public class CreateModel : PageModel
{
    private readonly AppDbContext _db;
    public CreateModel(AppDbContext db) => _db = db;

    [BindProperty]
    public Machine Machine { get; set; } = new() { Period = "Annually", Energy = "Electric" };

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        Machine.CreatedAt = DateTime.UtcNow;
        Machine.UpdatedAt = DateTime.UtcNow;
        _db.Machines.Add(Machine);
        await _db.SaveChangesAsync();
        return RedirectToPage("Details", new { id = Machine.Id });
    }
}
