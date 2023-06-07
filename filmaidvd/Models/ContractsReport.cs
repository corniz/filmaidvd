using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Autonuoma.Models.UzsakytaPaslaugaCE;

namespace Autonuoma.Models.ContractsReport
{
	public class SutartisA
	{
		[DisplayName("Sutartis")]
		public int Id { get; set; }

		[DisplayName("Data")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime SutartiesData { get; set; }
		//Data erorai nes ne datetime

		public string Vardas { get; set; }

		public string Pavarde { get; set; }

		public string AsmensKodas { get; set; }

		[DisplayName("Sudarytų sutarčių vertė")]
		public decimal Kaina { get; set; }

		[DisplayName("Užsakytų paslaugų vertė")]
		public decimal PaslauguKaina { get; set; }

		public decimal BendraSuma { get; set; }

		public decimal BendraSumaPaslaug { get; set; }
	}
	/// <summary>
	/// View model for whole report.
	/// </summary>
	public class Report
	{
		[DataType(DataType.DateTime)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime? DateFrom { get; set; }

		[DataType(DataType.DateTime)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime? DateTo { get; set; }

		public List<SutartisA> Sutartys { get; set; }

		public decimal VisoSumaSutartciu { get; set; }

		public decimal VisoSumaPaslaugu { get; set; }
		/// <summary>
		/// Related 'UzsakytaPaslauga' records.
		/// </summary>
		public IList<string> SelectedOrderOption { get; set; } = new List<string> { "Didejanciai", "Mazejanciai" };
		public string selected { get; set; }
	}
}