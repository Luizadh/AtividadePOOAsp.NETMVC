using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEventosCorporativos.Data;
using SistemaEventosCorporativos.Models;

namespace SistemaEventosCorporativos.Controllers
{
	public class ParticipanteController : Controller
	{
		private readonly AppDbContext _context;

		public ParticipanteController(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			var participantes = await _context.Participantes.ToListAsync();
			return View(participantes);
		}

		public async Task<IActionResult> Details(int id)
		{
			var participante = await _context.Participantes
				.Include(p => p.Inscricoes)
				.FirstOrDefaultAsync(p => p.Id == id);

			if (participante == null)
				return NotFound();

			return View(participante);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Participante participante)
		{
			if (ModelState.IsValid)
			{
				_context.Add(participante);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(participante);
		}

		public async Task<IActionResult> Edit(int id)
		{
			var participante = await _context.Participantes.FindAsync(id);
			if (participante == null)
				return NotFound();

			return View(participante);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, Participante participante)
		{
			if (id != participante.Id)
				return NotFound();

			if (ModelState.IsValid)
			{
				_context.Update(participante);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(participante);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var participante = await _context.Participantes.FindAsync(id);
			if (participante == null)
				return NotFound();

			return View(participante);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var participante = await _context.Participantes.FindAsync(id);
			if (participante != null)
			{
				_context.Participantes.Remove(participante);
				await _context.SaveChangesAsync();
			}
			return RedirectToAction(nameof(Index));
		}
	}
}