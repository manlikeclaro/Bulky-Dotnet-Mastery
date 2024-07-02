using BulkyWebRazor.Data;
using BulkyWebRazor.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor.Pages.Categories;

public class Index : PageModel
{
    private readonly ApplicationDbContext _db;
    public List<Category> CategoriesList { get; set; }

    public Index(ApplicationDbContext db)
    {
        _db = db;
    }

    public void OnGet()
    {
        CategoriesList = _db.Categories.ToList();
    }
}