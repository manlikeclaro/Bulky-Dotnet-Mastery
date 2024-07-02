using BulkyWebRazor.Data;
using BulkyWebRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor.Pages.Categories;

public class Delete : PageModel
{
    private readonly ApplicationDbContext _db;
    [BindProperty]
    public Category CategoryFromDb { get; set; }

    public Delete(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public IActionResult OnGet(int? Id)
    {
        if (Id != 0 && Id != null)
        {
            CategoryFromDb = _db.Categories.Find(Id);
            if (CategoryFromDb == null)
            {
                return NotFound();
            }
        }

        return Page();
    }

    public IActionResult OnPost()
    {
        _db.Categories.Remove(CategoryFromDb);
        _db.SaveChanges();
        
        TempData["Error"] = $"Category was deleted successfully!";
        return RedirectToPage(pageName: "/categories/index");
    }
}