using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


using Autonuoma.Repositories;
using Autonuoma.Models;

namespace Autonuoma.Controllers
{
	public class Filmu_tipaiController : Controller
	{
		/// <summary>
		/// This is invoked when either 'Index' action is requested or no action is provided.
		/// </summary>
		/// <returns>Entity list view.</returns>
		[HttpGet]
		public ActionResult Index()
		{
			var markes = Filmu_tipaiRepo.List();
			return View(markes);
		}

		/// <summary>
		/// This is invoked when creation form is first opened in browser.
		/// </summary>
		/// <returns>Creation form view.</returns>
		[HttpGet]
		public ActionResult Create()
		{
			var marke = new Filmu_tipai();
			return View(marke);
		}

		/// <summary>
		/// This is invoked when buttons are pressed in the creation form.
		/// </summary>
		/// <param name="marke">Entity model filled with latest data.</param>
		/// <returns>Returns creation from view or redirects back to Index if save is successfull.</returns>
		[HttpPost]
		public ActionResult Create(Filmu_tipai marke)
		{
			//form field validation passed?
			if (ModelState.IsValid)
			{
				Filmu_tipaiRepo.Insert(marke);

				//save success, go back to the entity list
				return RedirectToAction("Index");
			}

			//form field validation failed, go back to the form
			return View(marke);
		}

		/// <summary>
		/// This is invoked when editing form is first opened in browser.
		/// </summary>
		/// <param name="id">ID of the entity to edit.</param>
		/// <returns>Editing form view.</returns>
		[HttpGet]
		public ActionResult Edit(int id)
		{
			var marke = Filmu_tipaiRepo.Find(id);
			return View(marke);
		}

		/// <summary>
		/// This is invoked when buttons are pressed in the editing form.
		/// </summary>
		/// <param name="id">ID of the entity being edited</param>		
		/// <param name="marke">Entity model filled with latest data.</param>
		/// <returns>Returns editing from view or redirects back to Index if save is successfull.</returns>
		[HttpPost]
		public ActionResult Edit(int id, Filmu_tipai marke)
		{
			//form field validation passed?
			if (ModelState.IsValid)
			{
				Filmu_tipaiRepo.Update(marke);

				//save success, go back to the entity list
				return RedirectToAction("Index");
			}

			//form field validation failed, go back to the form
			return View(marke);
		}

		/// </summary>
		/// <param name="id">ID of the entity to delete.</param>
		/// <returns>Deletion form view.</returns>
		[HttpGet]
		public ActionResult Delete(int id)
		{
			var marke = Filmu_tipaiRepo.Find(id);
			return View(marke);
		}

		/// <summary>
		/// This is invoked when deletion is confirmed in deletion form
		/// </summary>
		/// <param name="id">ID of the entity to delete.</param>
		/// <returns>Deletion form view on error, redirects to Index on success.</returns>
		[HttpPost]
		public ActionResult DeleteConfirm(int id)
		{
			//try deleting, this will fail if foreign key constraint fails
			try
			{
				Filmu_tipaiRepo.Delete(id);

				//deletion success, redired to list form
				return RedirectToAction("Index");
			}
			//entity in use, deletion not permitted
			catch (MySql.Data.MySqlClient.MySqlException)
			{
				//enable explanatory message and show delete form
				ViewData["deletionNotPermitted"] = true;

				var marke = Filmu_tipaiRepo.Find(id);
				return View("Delete", marke);
			}
		}
	}
}
