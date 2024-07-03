using Bulky.DataAccess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        // Constructor for dependency injection of the database context
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: /Category/Index
        public IActionResult Index()
        {
            // Retrieve all categories from the database and pass them to the view
            List<Category> objCategoryList = _db.Categories.OrderBy(c => c.DisplayOrder).ToList();
            return View(objCategoryList);
        }

        // GET: /Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Category/Create
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            // Custom validation to ensure Name is not the same as DisplayOrder
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError(
                    key: "Name",
                    errorMessage: "The Category Name can't be identical to Display Order"
                );
            }

            // Check if the model is valid before saving to the database
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();

                // Use TempData to show a success message
                TempData["Success"] = $"Category \"{obj.Name}\" was created successfully!";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        // GET: /Category/Edit/Id
        public IActionResult Edit(int? id)
        {
            // Validate the Id parameter
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // Retrieve the category from the database
            Category? categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        // POST: /Category/Edit
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            // Check if the model is valid before updating the database
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();

                // Use TempData to show a success message
                TempData["Success"] = $"Category \"{obj.Name}\" was updated successfully!";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        // GET: /Category/Delete/Id
        public IActionResult Delete(int? id)
        {
            // Validate the Id parameter
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // Retrieve the category from the database
            Category? categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        // POST: /Category/Delete/Id
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            // Retrieve the category from the database
            Category? obj = _db.Categories.FirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            // Remove the category from the database
            _db.Categories.Remove(obj);
            _db.SaveChanges();

            // Use TempData to show a success message
            TempData["Error"] = $"Category \"{obj.Name}\" was deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
