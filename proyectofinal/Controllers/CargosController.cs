using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectofinal.Data;
using proyectofinal.Models;

namespace proyectofinal.Controllers
{
    public class CargosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CargosController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cargos.ToListAsync());
        }

        
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cargo cargo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cargo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cargo);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var cargo = await _context.Cargos.FindAsync(id);
            if (cargo == null) return NotFound();

            return View(cargo);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cargo cargo)
        {
            if (id != cargo.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(cargo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cargo);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var cargo = await _context.Cargos
                .FirstOrDefaultAsync(m => m.Id == id);

            if (cargo == null) return NotFound();

            return View(cargo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cargo = await _context.Cargos.FindAsync(id);

            if (cargo != null)
            {
                _context.Cargos.Remove(cargo);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
