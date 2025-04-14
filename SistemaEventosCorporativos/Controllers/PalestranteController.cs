using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaEventosCorporativos.Data;
using SistemaEventosCorporativos.Models;

namespace SistemaEventosCorporativos.Controllers
{
	public class PalestranteController : Controller
	{
		private readonly AppDbContext _context;

		public PalestranteController(AppDbContext context)
		{
			_context = context;
		}

		// GET: Palestrante
		public async Task<IActionResult> Index()
		{
			var palestrantes = _context.Palestrantes.Include(p => p.Evento);
			return View(await palestrantes.ToListAsync());
		}

		// GET: Palestrante/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null) return NotFound();

			var palestrante = await _context.Palestrantes
				.Include(p => p.Evento)
				.FirstOrDefaultAsync(p => p.Id == id);

			if (palestrante == null) return NotFound();

			return View(palestrante);
		}

		// GET: Palestrante/Create
		public IActionResult Create()
		{
			ViewBag.EventoId = new SelectList(_context.Eventos, "Id", "Nome");
			return View();
		}

		// POST: Palestrante/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Palestrante palestrante)
		{
			if (ModelState.IsValid)
			{
				_context.Add(palestrante);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			ViewBag.EventoId = new SelectList(_context.Eventos, "Id", "Nome", palestrante.EventoId);
			return View(palestrante);
		}

		// GET: Palestrante/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null) return NotFound();

			var palestrante = await _context.Palestrantes.FindAsync(id);
			if (palestrante == null) return NotFound();

			ViewBag.EventoId = new SelectList(_context.Eventos, "Id", "Nome", palestrante.EventoId);
			return View(palestrante);
		}

		// POST: Palestrante/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Palestrante palestrante)
		{
			if (id != palestrante.Id) return NotFound();

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(palestrante);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!_context.Palestrantes.Any(p => p.Id == id))
						return NotFound();
					else
						throw;
				}
				return RedirectToAction(nameof(Index));
			}

			ViewBag.EventoId = new SelectList(_context.Eventos, "Id", "Nome", palestrante.EventoId);
			return View(palestrante);
		}

		// GET: Palestrante/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null) return NotFound();

			var palestrante = await _context.Palestrantes
				.Include(p => p.Evento)
				.FirstOrDefaultAsync(p => p.Id == id);

			if (palestrante == null) return NotFound();

			return View(palestrante);
		}

		// POST: Palestrante/Delete/5
		[HttpPost, ActionName("DeleteConfirmed")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var palestrante = await _context.Palestrantes.FindAsync(id);
			if (palestrante != null)
			{
				_context.Palestrantes.Remove(palestrante);
				await _context.SaveChangesAsync();
			}

			return RedirectToAction(nameof(Index));
		}
	}
}