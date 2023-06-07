using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Autonuoma.Models
{
	public class Paslauga
	{
		public int InListId { get; set; }
		[DisplayName("Paslaugos ID")]
		public int Id { get; set; }

		[DisplayName("Pavadinimas")]
		[Required]
		public string Pavadinimas { get; set; }

		[DisplayName("Aprasymas")]
		[Required]
		public string Aprasymas { get; set; }
	}
}
