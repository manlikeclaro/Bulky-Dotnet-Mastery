using BulkyWebRazor.Data;
using BulkyWebRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor.Pages.Categories;

// [BindProperties]
public class Edit : PageModel
{
    private readonly ApplicationDbContext _db;
    [BindProperty] public Category CategoryFromDb { get; set; }

    public Edit(ApplicationDbContext db)
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
        if (ModelState.IsValid)
        {
            _db.Categories.Update(CategoryFromDb);
            _db.SaveChanges();

            TempData["Success"] = $"Category \"{CategoryFromDb.Name}\" was updated successfully!";
            return RedirectToPage(pageName: "/categories/index");
        }

        return Page();
    }
}