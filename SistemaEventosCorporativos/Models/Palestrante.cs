using System.ComponentModel.DataAnnotations;

namespace SistemaEventosCorporativos.Models
{
	public class Palestrante
	{
		public int Id { get; set; }

		[Required]
		public string Nome { get; set; }

		public string Curriculo { get; set; }

		// Um palestrante pertence a um evento
		public int EventoId { get; set; }

		
		// [Required]
		public Evento? Evento { get; set; }
	}
}