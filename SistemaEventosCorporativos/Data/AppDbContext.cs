using Microsoft.EntityFrameworkCore;
using SistemaEventosCorporativos.Models;

namespace SistemaEventosCorporativos.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions options)
			: base(options)
		{
		}

		public DbSet<Evento> Eventos { get; set; }
		public DbSet<Palestrante> Palestrantes { get; set; }
		public DbSet<Participante> Participantes { get; set; }
		public DbSet<Inscricao> Inscricoes { get; set; }
	}
}