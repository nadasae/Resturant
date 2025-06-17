using Microsoft.AspNetCore.Mvc;
using Resturant.BL.Contracts;
using Resturant.BL.Features.MenuItems.Requests;


namespace Resturant.Presentation.Controllers
{
    public class MenuItemController : Controller
    {
        private readonly IMenuItemServices _menuItemService;

        public MenuItemController(IMenuItemServices menuItemService)
        {
            _menuItemService = menuItemService;
        }

        // GET: MenuItem
        public async Task<IActionResult> Index()
        {
            await _menuItemService.ResetAvailabilityDailyAsync();
            ViewBag.Available = await _menuItemService.UpdateAvailabilityBasedOnOrderCountAsync();
            var result = await _menuItemService.GetAllAsync();
            if (!result.IsSuccess)
                ModelState.AddModelError("", result.Error);

            return View(result.Data);
        }

        // GET: MenuItem/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var result = await _menuItemService.GetByIdAsync(id);
            if (!result.IsSuccess)
                return NotFound(result.Error);

            return View(result.Data);
        }

        // GET: MenuItem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MenuItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddMenuItemRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = await _menuItemService.AddAsync(model);
                if (result.IsSuccess)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Error);
            }
            return View(model);
        }

        // GET: MenuItem/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _menuItemService.GetByIdAsync(id);
            if (!result.IsSuccess)
                return NotFound();

            var menuItem = result.Data;
            var editModel = new UpdateMenuItemRequest
            (
                menuItem.Id,
                menuItem.Name,
                menuItem.Price,
                menuItem.CategoryId
            );

            return View(editModel);
        }

        // POST: MenuItem/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateMenuItemRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = await _menuItemService.UpdateAsync(model);
                if (result.IsSuccess)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Error);
            }
            return View(model);
        }

        // GET: MenuItem/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _menuItemService.GetByIdAsync(id);
            if (!result.IsSuccess)
                return NotFound();

            return View(result.Data);
        }

        // POST: MenuItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _menuItemService.DeleteAsync(new DeleteMenuItemRequest(id));
            if (result.IsSuccess)
                return RedirectToAction(nameof(Index));
            return View();
        }
    }
}