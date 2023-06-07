using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using Autonuoma.Repositories;
using Autonuoma.Models;
using Newtonsoft.Json;
using CostractsReport = Autonuoma.Models.ContractsReport;


/// <summary>
/// Controller for producing reports.
/// </summary>
public class ReportsController : Controller
{
	/// <summary>
	/// Produces contracts report.
	/// </summary>
	/// <param name="dateFrom">Starting date. Can be null.</param>
	/// <param name="dateTo">Ending date. Can be null.</param>
	/// <returns>Report view.</returns>
	[HttpGet]
	public ActionResult Contracts(DateTime? dateFrom, DateTime? dateTo, string selected)
	{
		var report = new CostractsReport.Report();
		report.DateFrom = dateFrom;
		report.DateTo = dateTo?.AddHours(23).AddMinutes(59).AddSeconds(59); //move time of end date to end of day
		report.selected = selected;

		var orderByClause = "RAND()"; // default order
		if (report.selected == "Mazejanciai") orderByClause = "vie.pavarde DESC";
		else if (report.selected == "Didejanciai") orderByClause = "vie.pavarde ASC";

		report.Sutartys = AtaskaitaRepo.GetContracts(report.DateFrom, report.DateTo, orderByClause);

		foreach (var item in report.Sutartys)
		{
			report.VisoSumaSutartciu += item.Kaina;
			report.VisoSumaPaslaugu += item.PaslauguKaina;
		}


		return View(report);
	}

}
