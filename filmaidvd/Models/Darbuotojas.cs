using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Org.Ktu.Isk.P175B602.Autonuoma;
using Autonuoma;

namespace Autonuoma.Models
{
	public class Darbuotojas
	{
		public int InListId { get; set; }
		[DisplayName("Darbuotojo Nr")]
		public int Id { get; set; }

		[DisplayName("Vardas")]
		[Required]
		public string Vardas { get; set; }

		[DisplayName("Pavarde")]
		[Required]
		public string Pavarde { get; set; }
	}
}
