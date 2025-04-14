using System.ComponentModel.DataAnnotations;

namespace SistemaEventosCorporativos.Models
{
	public class Evento
	{
		public int Id { get; set; }

		[Required]
		public string Nome { get; set; }

		public string Local { get; set; }

		public DateTime Data { get; set; }

		// Um evento pode ter vários palestrantes

		public ICollection<Palestrante> Palestrantes { get; set; } = new List<Palestrante>();


		public List<Inscricao> Inscricoes { get; set; } = new List<Inscricao>();



	}
}