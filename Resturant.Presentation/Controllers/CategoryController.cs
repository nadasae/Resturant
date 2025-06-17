using Microsoft.AspNetCore.Mvc;
using Resturant.BL.Contracts;
using Resturant.BL.Features.Categories.Requests;


namespace Resturant.Presentation.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryServices _categoryService;

        public CategoryController(ICategoryServices categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAllCategoriesAsync();
            if (!result.IsSuccess)
                ModelState.AddModelError("", result.Error);

            return View(result.Data);
        }

        // GET: Category/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var request = new GetCategoryByIdRequest(id);
            var result = await _categoryService.GetCategoryByIdAsync(request);
            if (!result.IsSuccess)
                return NotFound(result.Error);

            return View(result.Data);
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddCategoryRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.AddCategoryAsync(model);
                if (result.IsSuccess)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Error);
            }
            return View(model);
        }

        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var request = new GetCategoryByIdRequest(id);
            var result = await _categoryService.GetCategoryByIdAsync(request);
            if (!result.IsSuccess)
                return NotFound();

            var category = result.Data;

            // Fetch all available menu items
            int[] allMenuItems = { 1, 2, 3, 4, 5 }; // Assuming you have this method
            ViewBag.MenuItems = allMenuItems;

            var editModel = new UpdateCategoryRequest
            (
                category.Id,
                category.Name,
               
                category.menuItemsIds?.ToList() ?? new List<int>()
            );

            return View(editModel);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateCategoryRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.UpdateCategoryAsync(model);
                if (result.IsSuccess)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Error);
            }

            // Repopulate menu items for validation errors
            int[] allMenuItems = { 1, 2, 3, 4, 5 };
            ViewBag.MenuItems = allMenuItems;

            return View(model);
        }

        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var request = new GetCategoryByIdRequest(id);
            var result = await _categoryService.GetCategoryByIdAsync(request);
            if (!result.IsSuccess)
                return NotFound();

            return View(result.Data);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(new DeleteCategoryRequest(id));
            if (result.IsSuccess)
                return RedirectToAction(nameof(Index));
            return View();
        }
    }
}
