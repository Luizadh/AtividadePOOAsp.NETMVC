using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaEventosCorporativos.Models
{
	public class Inscricao
	{
		public int Id { get; set; }

		public int ParticipanteId { get; set; }


		//[Required]
		public Participante? Participante { get; set; }

		public int EventoId { get; set; }
		public Evento? Evento { get; set; }

		public DateTime DataInscricao { get; set; } = DateTime.Now;
	}
}