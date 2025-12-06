using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using proyectofinal.Data;
using proyectofinal.Models;

namespace proyectofinal.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmpleadosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _context.Empleados
                .Include(e => e.Departamento)
                .Include(e => e.Cargo)
                .ToListAsync();

            return View(lista);
        }

        public IActionResult Create()
        {
            ViewBag.Departamentos = _context.Departamentos.ToList();
            ViewBag.Cargos = _context.Cargos.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Empleado empleado)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departamentos = _context.Departamentos.ToList();
                ViewBag.Cargos = _context.Cargos.ToList();
                return View(empleado);
            }

            _context.Empleados.Add(empleado);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null) return NotFound();

            ViewBag.Departamentos = _context.Departamentos.ToList();
            ViewBag.Cargos = _context.Cargos.ToList();

            return View(empleado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Empleado empleado)
        {
            if (id != empleado.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Departamentos = _context.Departamentos.ToList();
                ViewBag.Cargos = _context.Cargos.ToList();
                return View(empleado);
            }

            _context.Empleados.Update(empleado);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}

