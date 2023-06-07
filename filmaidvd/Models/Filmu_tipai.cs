namespace Autonuoma.Models
{
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using Microsoft.AspNetCore.Mvc.Rendering;


	/// <summary>
	/// Model for 'Marke' entity.
	/// </summary>
	public class Filmu_tipai
	{
		[DisplayName("Id")]
		public int Id { get; set; }

		[DisplayName("Pavadinimas")]
		[Required]
		public string Pavadinimas { get; set; }
	}
}
