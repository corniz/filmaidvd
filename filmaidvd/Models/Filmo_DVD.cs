using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Org.Ktu.Isk.P175B602.Autonuoma;
using Autonuoma;

namespace Autonuoma.Models
{
	public class Filmo_DVD
	{
	}
	/// <summary>
	/// 'Automobilis' in list form.
	/// </summary>
	public class Filmo_DVDL
	{
		[DisplayName("Id")]
		public int Id { get; set; }

		[DisplayName("Kūrėjai")]
		public string Kurejai { get; set; }

		[DisplayName("Žanras")]
		public string Zanras { get; set; }

		[DisplayName("Apribojimai")]
		public string Apribojimai { get; set; }
	}

	/// <summary>
	/// 'Automobilis' in create and edit forms.
	/// </summary>
	public class Filmo_DVDCE
	{
		/// <summary>
		/// Automobilis.
		/// </summary>
		public class Filmo_DVDM
		{
			[DisplayName("Id")]
			[Required]
			public int Id { get; set; }

			//float
			//currency?
			[DisplayName("Vertė")]
			[Required]
			[DataType(DataType.Currency)]
			public double Verte { get; set; }

			[DisplayName("Aktoriai")]
			[Required]
			public string Aktoriai { get; set; }

			[DisplayName("Įvertinimas")]
			[Required]
			public int Ivertinimas { get; set; }

			[DisplayName("Kūrėjai")]
			[Required]
			public string Kurejai { get; set; }

			[DisplayName("Filmo aprašymas")]
			[Required]
			public string Aprasymas { get; set; }

			[DisplayName("Filmo ilgumas")]
			[Required]
			public int Ilgumas { get; set; }

			[DisplayName("Filmo kalba")]
			[Required]
			public string Kalba { get; set; }

			[DisplayName("Rezoliucija")]
			[Required]
			public string Rezoliucija { get; set; }

			[DisplayName("Kiekis")]
			[Required]
			public int Kiekis { get; set; }

			[DisplayName("Žanras")]
			[Required]
			public int Zanras { get; set; }

			[DisplayName("Amžiaus apribojimai")]
			[Required]
			public int Apribojimai { get; set; }
		}

		/// <summary>
		/// Select lists for making drop downs for choosing values of entity fields.
		/// </summary>
		public class ListsM
		{
			public IList<SelectListItem> Zanras { get; set; }
			public IList<SelectListItem> Apribojimai { get; set; }

		}

		/// <summary>
		/// Automobilis.
		/// </summary>
		public Filmo_DVDM Filmo_DVD { get; set; } = new Filmo_DVDM();

		/// <summary>
		/// Lists for drop down controls.
		/// </summary>
		public ListsM Lists { get; set; } = new ListsM();
	}


	/// <summary>
	/// 'AutoBusena' enumerator in lists.
	/// </summary>
	public class Zanras
	{
		public int Id { get; set; }

		public string Pavadinimas { get; set; }
	}

	/// <summary>
	/// 'PavaruDeze' enumerator in lists.
	/// </summary>
	public class Apribojimai
	{
		public int Id { get; set; }

		public string Pavadinimas { get; set; }
	}
}
