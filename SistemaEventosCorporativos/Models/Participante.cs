using System.ComponentModel.DataAnnotations;

namespace SistemaEventosCorporativos.Models
{
	public class Participante
	{
		public int Id { get; set; }

		[Required]
		public string Nome { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		// Navegação: uma lista de inscrições feitas por este participante

		[Required]
		public ICollection<Inscricao> Inscricoes { get; set; } = new List<Inscricao>();
	}
}