using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEventosCorporativos.Data;
using SistemaEventosCorporativos.Models;

namespace SistemaEventosCorporativos.Controllers
{
	public class EventoController : Controller
	{
		private readonly AppDbContext _context;

		public EventoController(AppDbContext context)
		{
			_context = context;
		}

		// GET: Evento
		public async Task<IActionResult> Index()
		{
			var eventos = await _context.Eventos
				.Include(e => e.Palestrantes)
				.Include(e => e.Inscricoes)
				.ToListAsync();
			return View(eventos);
		}

		// GET: Evento/Details/5


		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var evento = await _context.Eventos
				.Include(e => e.Palestrantes)  // Carrega os palestrantes relacionados
				.Include(e => e.Inscricoes)    // Carrega as inscrições relacionadas
				.FirstOrDefaultAsync(m => m.Id == id);

			if (evento == null)
			{
				return NotFound();
			}

			return View(evento);
		}



		// GET: Evento/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Evento/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Nome, Local, Data")] Evento evento)
		{
			if (ModelState.IsValid)
			{
				_context.Add(evento);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(evento);
		}

		// GET: Evento/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var evento = await _context.Eventos.FindAsync(id);
			if (evento == null)
			{
				return NotFound();
			}
			return View(evento);
		}

		// POST: Evento/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id, Nome, Local, Data")] Evento evento)
		{
			if (id != evento.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(evento);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!EventoExists(evento.Id))
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
			return View(evento);
		}

		// GET: Evento/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var evento = await _context.Eventos
				.FirstOrDefaultAsync(m => m.Id == id);
			if (evento == null)
			{
				return NotFound();
			}

			return View(evento);
		}

		// POST: Evento/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var evento = await _context.Eventos.FindAsync(id);
			_context.Eventos.Remove(evento);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool EventoExists(int id)
		{
			return _context.Eventos.Any(e => e.Id == id);
		}



	}
}