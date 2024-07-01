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
            
            TempData["Success"] = $"Category \"{obj.Name}\" was created successfully!";
            return RedirectToAction(actionName: "Index", controllerName: "Category");
        }

        return View();
    }

    public IActionResult Edit(int? Id)
    {
        if (Id == 0 || Id == null)
        {
            return NotFound();
        }

        Category? CategoryFromDb = _db.Categories.Find(Id);
        // Category? CategoryFromDb = _db.Categories.FirstOrDefault(u=>u.Id==Id);
        if (CategoryFromDb == null)
        {
            return NotFound();
        }

        return View(CategoryFromDb);
    }

    [HttpPost]
    public IActionResult Edit(Category obj)
    {
        if (ModelState.IsValid)
        {
            _db.Categories.Update(obj);
            _db.SaveChanges();
            
            TempData["Success"] = $"Category \"{obj.Name}\" was updated successfully!";
            return RedirectToAction(actionName: "Index", controllerName: "Category");
        }

        return View();
    }

    public IActionResult Delete(int? Id)
    {
        if (Id == 0 || Id == null)
        {
            return NotFound();
        }

        // Category? CategoryFromDb = _db.Categories.FirstOrDefault(u => u.Id == Id);
        Category? CategoryFromDb = _db.Categories.Find(Id);
        if (CategoryFromDb == null)
        {
            return NotFound();
        }

        return View(CategoryFromDb);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? Id)
    {
        Category? obj = _db.Categories.FirstOrDefault(u => u.Id == Id);
        if (obj == null)
        {
            return NotFound();
        }

        _db.Categories.Remove(obj);
        _db.SaveChanges();

        TempData["Success"] = $"Category \"{obj.Name}\" was deleted successfully!";
        return RedirectToAction(actionName: "Index", controllerName: "Category");
    }
}