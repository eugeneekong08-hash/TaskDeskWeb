using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskDeskWeb.Data;
using TaskDeskWeb.Models;

namespace TaskDeskWeb.Pages;

public class IndexModel : PageModel
{
    private readonly AppDbContext _db;

    public IndexModel(AppDbContext db)
    {
        _db = db;
    }

    public List<TaskItem> Tasks { get; set; } = new();

    [BindProperty]
    public string NewTitle { get; set; } = "";

    public async Task OnGetAsync()
    {
        Tasks = await _db.Tasks.ToListAsync();
    }

    public async Task<IActionResult> OnPostAddAsync()
    {
        if (string.IsNullOrWhiteSpace(NewTitle))
        {
            return RedirectToPage();
        }

        _db.Tasks.Add(new TaskItem { Title = NewTitle });
        await _db.SaveChangesAsync();

        return RedirectToPage();
    }
    public async Task<IActionResult> OnPostToggleAsync(int id)
    {
        var task = await _db.Tasks.FindAsync(id);

        if (task != null)
        {
            task.IsDone = !task.IsDone;
            await _db.SaveChangesAsync();
        }

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var task = await _db.Tasks.FindAsync(id);

        if (task != null)
        {
            _db.Tasks.Remove(task);
            await _db.SaveChangesAsync();
        }

        return RedirectToPage();
    }
}

