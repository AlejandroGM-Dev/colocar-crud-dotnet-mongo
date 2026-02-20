using ColocarCrud.Application.Users;
using Microsoft.AspNetCore.Mvc;

namespace ColocarCrud.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _service;

        public UsersController(IUserService service) => _service = service;

        // GET: /Users
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var users = await _service.GetAllAsync(ct);
            return View(users);
        }

        // GET: /Users/Create
        public IActionResult Create() => View();

        // POST: /Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserDto dto, CancellationToken ct)
        {
            try
            {
                await _service.CreateAsync(dto, ct);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(dto);
            }
        }

        // GET: /Users/Edit/{id}
        public async Task<IActionResult> Edit(string id, CancellationToken ct)
        {
            var u = await _service.GetByIdAsync(id, ct);
            if (u is null) return NotFound();

            return View(new UpdateUserDto(u.Id, u.Email, u.FullName));
        }

        // POST: /Users/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateUserDto dto, CancellationToken ct)
        {
            try
            {
                var ok = await _service.UpdateAsync(dto, ct);
                if (!ok) return NotFound();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(dto);
            }
        }

        // POST: /Users/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, CancellationToken ct)
        {
            await _service.DeleteAsync(id, ct);
            return RedirectToAction(nameof(Index));
        }
    }
}
