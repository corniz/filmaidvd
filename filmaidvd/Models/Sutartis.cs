using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Org.Ktu.Isk.P175B602.Autonuoma;
using Autonuoma;

namespace Autonuoma.Models
{
	public class Sutartis
	{
		[DisplayName("Sutarties id")]
		public int Id { get; set; }

		[DisplayName("Data")]
		[Required]
		public string Data { get; set; }
		[DisplayName("Galiojimas")]
		[Required]
		public string Galiojimas { get; set; }
		[DisplayName("Laikas")]
		[Required]
		public string Laikas { get; set; }

		[DisplayName("Kaina")]
		[Required]
		public double Kaina { get; set; }
	}
}
