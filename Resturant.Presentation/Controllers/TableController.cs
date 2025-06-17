using Microsoft.AspNetCore.Mvc;
using Resturant.BL.Contracts;
using Resturant.BL.Features.Tables.Requests;

namespace Resturant.Presentation.Controllers
{
        public class TableController : Controller
        {
            private readonly ITableService _tableService;

            public TableController(ITableService tableService)
            {
                _tableService = tableService;
            }

            // GET: Table
            public async Task<IActionResult> Index()
            {
                var result = await _tableService.GetAvailableTablesAsync();
                if (!result.IsSuccess)
                    ModelState.AddModelError("", result.Error);

                return View(result.Data);
            }

            // GET: Table/Details/5
            public async Task<IActionResult> Details(int id)
            {
                var request = new GetTableByIdRequest( id );
                var result = await _tableService.GetByIdAsync(request);
                if (!result.IsSuccess)
                    return NotFound(result.Error);

                return View(result.Data);
            }

            // GET: Table/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: Table/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(AddTableRequest model)
            {
                if (ModelState.IsValid)
                {
                    var result = await _tableService.AddAsync(model);
                    if (result.IsSuccess)
                        return RedirectToAction(nameof(Index));
                    ModelState.AddModelError("", result.Error);
                }
                return View(model);
            }

            // GET: Table/Edit/5
            public async Task<IActionResult> Edit(int id)
            {
                var request = new GetTableByIdRequest(id);
                var result = await _tableService.GetByIdAsync(request);
                if (!result.IsSuccess)
                    return NotFound();

                var table = result.Data;
            var editModel = new UpdateTableRequest
            (
                 id,
                table.Number,
                 table.Capacity,
                 table.OrdersIds ?? new List<int>()
            );

                return View(editModel);
            }

            // POST: Table/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(UpdateTableRequest model)
            {
                if (ModelState.IsValid)
                {
                    var result = await _tableService.UpdateAsync(model);
                    if (result.IsSuccess)
                        return RedirectToAction(nameof(Index));
                    ModelState.AddModelError("", result.Error);
                }
                return View(model);
            }

            // GET: Table/Delete/5
            public async Task<IActionResult> Delete(int id)
            {
                var request = new GetTableByIdRequest (id );
                var result = await _tableService.GetByIdAsync(request);
                if (!result.IsSuccess)
                    return NotFound();

                return View(result.Data);
            }

            // POST: Table/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var result = await _tableService.DeleteAsync(new DeleteTableRequest ( id ));
                if (result.IsSuccess)
                    return RedirectToAction(nameof(Index));
                return View();
            }
        }
    }

