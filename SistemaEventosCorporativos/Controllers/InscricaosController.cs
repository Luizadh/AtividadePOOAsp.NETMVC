using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using SistemaEventosCorporativos.Data;
using SistemaEventosCorporativos.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaEventosCorporativos.Data;
using SistemaEventosCorporativos.Models;

namespace SistemaEventosCorporativos.Controllers
{
	public class InscricaoController : Controller
	{
		private readonly AppDbContext _context;

		public InscricaoController(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			var inscricoes = _context.Inscricoes
				.Include(i => i.Participante)
				.Include(i => i.Evento);
			return View(await inscricoes.ToListAsync());
		}

		public IActionResult Create()
		{
			ViewBag.ParticipanteId = new SelectList(_context.Participantes, "Id", "Nome");
			ViewBag.EventoId = new SelectList(_context.Eventos, "Id", "Nome");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Inscricao inscricao)
		{
			if (ModelState.IsValid)
			{
				_context.Add(inscricao);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewBag.ParticipanteId = new SelectList(_context.Participantes, "Id", "Nome", inscricao.ParticipanteId);
			ViewBag.EventoId = new SelectList(_context.Eventos, "Id", "Nome", inscricao.EventoId);
			return View(inscricao);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var inscricao = await _context.Inscricoes
				.Include(i => i.Participante)
				.Include(i => i.Evento)
				.FirstOrDefaultAsync(m => m.Id == id);

			if (inscricao == null)
			{
				return NotFound();
			}

			return View(inscricao);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var inscricao = await _context.Inscricoes.FindAsync(id);
			_context.Inscricoes.Remove(inscricao);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
	}
}