using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectofinal.Data;
using proyectofinal.Models;

namespace proyectofinal.Controllers
{
    public class DepartamentosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartamentosController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        public async Task<IActionResult> Index()
        {
            return View(await _context.Departamentos.ToListAsync());
        }

        
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(departamento);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var departamento = await _context.Departamentos.FindAsync(id);
            if (departamento == null) return NotFound();

            return View(departamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Departamento departamento)
        {
            if (id != departamento.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(departamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(departamento);
        }

 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var departamento = await _context.Departamentos
                .FirstOrDefaultAsync(m => m.Id == id);

            if (departamento == null) return NotFound();

            return View(departamento);
        }

      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departamento = await _context.Departamentos.FindAsync(id);

            if (departamento != null)
            {
                _context.Departamentos.Remove(departamento);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
