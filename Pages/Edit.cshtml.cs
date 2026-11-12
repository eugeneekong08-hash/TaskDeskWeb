using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskDeskWeb.Data;

namespace TaskDeskWeb.Pages;

public class EditModel : PageModel
{
    private readonly AppDbContext _db;

    public EditModel(AppDbContext db)
    {
        _db = db;
    }

    [BindProperty]
    public int Id { get; set; }

    [BindProperty]
    public string Title { get; set; } = "";

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var task = await _db.Tasks.FindAsync(id);

        if (task == null)
        {
            return RedirectToPage("Index");
        }

        Id = task.Id;
        Title = task.Title;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrWhiteSpace(Title))
        {
            return Page();
        }

        var task = await _db.Tasks.FindAsync(Id);

        if (task != null)
        {
            task.Title = Title;
            await _db.SaveChangesAsync();
        }

        return RedirectToPage("Index");
    }
}