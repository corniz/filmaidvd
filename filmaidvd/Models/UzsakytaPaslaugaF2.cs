using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Autonuoma.Models
{
	/// <summary>
	/// 'Sutartis' in list form.
	/// </summary>
	public class UzsakytaPaslaugaL
	{
		[DisplayName("Nr.")]
		public int Nr { get; set; }

		[DisplayName("Paslaugos Kaina")]
		public double PaslaugosKaina { get; set; }

		[DisplayName("Uzsakymo data")]
		public string Data { get; set; }

		[DisplayName("Sutarties ID")]
		public int Sutartis { get; set; }

		[DisplayName("Laikas")]
		public string Laikas { get; set; }
	}


	/// <summary>
	/// 'Sutartis' in create and edit forms.
	/// </summary>
	public class UzsakytaPaslaugaCE
	{
		/// <summary>
		/// Entity data.
		/// </summary>
		public class UzsakytaPaslaugaM
		{
			[DisplayName("Kiekis")]
			[Required]
			[Range(1, int.MaxValue)]
			public int Kiekis { get; set; }

			[DisplayName("Kaina")]
			[Required]
			public double Kaina { get; set; }

			[DisplayName("Data")]
			[Required]
			public string Data { get; set; }

			public int InListId { get; set; }

			[DisplayName("PaslaugaFk")]
			[Required]
			public int FkPaslauga { get; set; }

			[DisplayName("SutartisFk")]
			[Required]
			public int FkSutartis { get; set; }
		}

		/// <summary>
		/// Representation of 'UzsakytaPaslauga' entity in 'Sutartis' edit form.
		/// </summary>
		public class SutartisM
		{
			/// <summary>
			/// ID of the record in the form. Is used when adding and removing records.
			/// </summary>
			[DisplayName("Nr")]
			public int Nr { get; set; }

			public int InListId { get; set; }

			[DisplayName("Data")]
			[Required]
			public string SutartiesData { get; set; }

			[DisplayName("Kaina")]
			[Required]
			public double Kaina { get; set; } //float?

			[DisplayName("Garantinio galiojimas")]
			[Required]
			public string Galiojimas { get; set; }

			[DisplayName("Laikas")]
			[Required]
			public string Laikas { get; set; }

			[DisplayName("Darbuotojas")]
			[Required]
			public int FkDarbuotojas { get; set; }

			[DisplayName("Klientas")]
			[Required]
			public Int64 FkKlientas { get; set; }

			[DisplayName("Paslauga")]
			[Required]
			public string Paslauga { get; set; } // gali buti erroru

		}

		/// <summary>
		/// Select lists for making drop downs for choosing values of entity fields.
		/// </summary>
		public class ListsM
		{
			public IList<SelectListItem> Sutartis { get; set; }
			//public IList<SelectListItem> Automobiliai { get; set; }
			public IList<SelectListItem> Paslaugos { get; set; }
		}


		/// <summary>
		/// Sutartis.
		/// </summary>
		public UzsakytaPaslaugaM UzsakytosPaslaugos { get; set; } = new UzsakytaPaslaugaM();

		/// <summary>
		/// Related 'UzsakytaPaslauga' records.
		/// </summary>
		public IList<SutartisM> Sutartis { get; set; } = new List<SutartisM>();

		/// <summary>
		/// Lists for drop down controls.
		/// </summary>
		public ListsM Lists { get; set; } = new ListsM();
	}


	/// <summary>
	/// 'SutartiesBusena' enumerator in lists.
	/// /// </summary>
	/*public class SutartiesBusena
	{
		public int Id { get; set; }

		public string Name { get; set; }
	}*/
}
