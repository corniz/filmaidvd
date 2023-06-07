namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;


/// <summary>
/// <para>This controller is used when no controller is specified in the request URL.</para>
/// <para>The reques URL is interpretted with the following template http://server-address/controller/action/[...suffix...]</para>
/// <para>Static and inner members are thread safe, other members are not thread safe.</para>
/// </summary>
public class HomeController : Controller
{
	/// <summary>
	/// Handles 'Index' action. Is also invoked when no action is specified.
	/// </summary>
	/// <returns>View to render.</returns>
	[HttpGet]
	public ActionResult Index()
	{
		return View();
	}
}
