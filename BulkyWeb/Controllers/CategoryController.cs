using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }

    // [HttpGet]
    public IActionResult Index()
    {
        // var objCategoryList = _db.Categories.ToList();
        List<Category> objCategoryList = _db.Categories.ToList();
        return View(objCategoryList);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Category obj)
    {
        if (obj.Name == Convert.ToString(obj.DisplayOrder))
        {
            ModelState.AddModelError(
                key: "Name",
                errorMessage: "The Category Name can't be identical to Display Order");
        }

        if (ModelState.IsValid)
        {
            _db.Categories.Add(obj);
            _db.SaveChanges();
            return RedirectToAction(actionName: "Index", controllerName: "Category");
        }

        return View();
    }
}