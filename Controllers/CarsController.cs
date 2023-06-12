using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SingletonPattern_Log_CRUD.Data;
using SingletonPattern_Log_CRUD.Models;
using SingletonPattern_Log_CRUD.Services;

namespace SingletonPattern_Log_CRUD.Controllers
{
    public class CarsController : Controller
    {
        private readonly SingletonPattern_Log_CRUDContext _context;

        public CarsController(SingletonPattern_Log_CRUDContext context)
        {
            _context = context;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            LoggerService.Instance.Log(
                HttpContext.Connection.RemoteIpAddress?.ToString() != null ? HttpContext.Connection.RemoteIpAddress?.ToString() : "-",
                $"{nameof(Index)}",
                $"{nameof(CarsController)}.{nameof(Index)}"
                );

            return _context.Car != null ?
                          View(await _context.Car.ToListAsync()) :
                          Problem("Entity set 'SingletonPattern_Log_CRUDContext.Car'  is null.");
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            LoggerService.Instance.Log(
                HttpContext.Connection.RemoteIpAddress?.ToString() != null ? HttpContext.Connection.RemoteIpAddress?.ToString() : "-",
                $"{nameof(Details)}",
                $"{nameof(CarsController)}.{nameof(Details)}.{id}"
                );

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            LoggerService.Instance.Log(
                HttpContext.Connection.RemoteIpAddress?.ToString() != null ? HttpContext.Connection.RemoteIpAddress?.ToString() : "-",
                $"{nameof(Create)}",
                $"{nameof(CarsController)}.{nameof(Create)}.View"
                );
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Model,Color,Brand,Year")] Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            LoggerService.Instance.Log(
                HttpContext.Connection.RemoteIpAddress?.ToString() != null ? HttpContext.Connection.RemoteIpAddress?.ToString() : "-",
                $"{nameof(Create)}",
                $"{nameof(CarsController)}.{nameof(Create)}"
                );

            return View(car);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            LoggerService.Instance.Log(
                HttpContext.Connection.RemoteIpAddress?.ToString() != null ? HttpContext.Connection.RemoteIpAddress?.ToString() : "-",
                $"{nameof(Edit)}",
                $"{nameof(CarsController)}.{nameof(Edit)}.{id}.View"
                );
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Model,Color,Brand,Year")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            LoggerService.Instance.Log(
                HttpContext.Connection.RemoteIpAddress?.ToString() != null ? HttpContext.Connection.RemoteIpAddress?.ToString() : "-",
                $"{nameof(Edit)}",
                $"{nameof(CarsController)}.{nameof(Edit)}.{id}"
                );

            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            LoggerService.Instance.Log(
                HttpContext.Connection.RemoteIpAddress?.ToString() != null ? HttpContext.Connection.RemoteIpAddress?.ToString() : "-",
                $"{nameof(Delete)}",
                $"{nameof(CarsController)}.{nameof(Delete)}.{id}.View"
                );

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Car == null)
            {
                return Problem("Entity set 'SingletonPattern_Log_CRUDContext.Car'  is null.");
            }
            var car = await _context.Car.FindAsync(id);
            if (car != null)
            {
                _context.Car.Remove(car);
            }

            await _context.SaveChangesAsync();

            LoggerService.Instance.Log(
                HttpContext.Connection.RemoteIpAddress?.ToString() != null ? HttpContext.Connection.RemoteIpAddress?.ToString() : "-",
                $"{nameof(DeleteConfirmed)}",
                $"{nameof(CarsController)}.{nameof(DeleteConfirmed)}.{id}"
                );

            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return (_context.Car?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
