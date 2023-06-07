using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using Autonuoma.Repositories;
using Autonuoma.Models;
using Newtonsoft.Json;
using static Autonuoma.Models.UzsakytaPaslaugaCE;

namespace Autonuoma.Controllers
{
	public class UzsakytaPaslaugaF2Controller : Controller
	{
		/// <summary>
		/// This is invoked when either 'Index' action is requested or no action is provided.
		/// </summary>
		/// <returns>Entity list view.</returns>
		[HttpGet]
		public ActionResult Index()
		{
			return View(UzsakytaPaslaugaF2Repo.ListUzsakytaPaslauga());
		}

		/// <summary>
		/// This is invoked when creation form is first opened in a browser.
		/// </summary>
		/// <returns>Entity creation form.</returns>
		[HttpGet]
		public ActionResult Create()
		{
			var sutCE = new UzsakytaPaslaugaCE();
			sutCE.UzsakytosPaslaugos.Data = DateTime.Now.ToString();
			/*sutCE.UzsakytosPaslaugos.Data = DateTime.Now;
			sutCE.UzsakytosPaslaugos. = DateTime.Now;
			sutCE.UzsakytosPaslaugos = DateTime.Now;*/

			PopulateLists(sutCE);

			return View(sutCE);
		}


		/// <summary>
		/// This is invoked when buttons are pressed in the creation form.
		/// </summary>
		/// <param name="save">If not null, indicates that 'Save' button was clicked.</param>
		/// <param name="add">If not null, indicates that 'Add' button was clicked.</param>
		/// <param name="remove">If not null, indicates that 'Remove' button was clicked and contains in-list-id of the item to remove.</param>
		/// <param name="sutCE">Entity view model filled with latest data.</param>
		/// <returns>Returns creation from view or redirets back to Index if save is successfull.</returns>
		[HttpPost]
		public ActionResult Create(int? save, int? add, int? remove, UzsakytaPaslaugaCE sutCE)
		{
			//addition of new 'UzsakytosPaslaugos' record was requested?
			if (add != null)
			{
				//add entry for the new record
				var up =
					new UzsakytaPaslaugaCE.SutartisM
					{
						InListId =
							sutCE.Sutartis.Count > 0 ?
							
							sutCE.Sutartis.Max(it => it.InListId) + 1 :
							1,
						SutartiesData = null,
						Galiojimas= null,
						Laikas = null,
						/*FkKlientas = null,
						FkDarbuotojas = null,*/
						Kaina = 0,
						Paslauga= null
						
					};
				sutCE.Sutartis.Add(up);

				//make sure @Html helper is not reusing old model state containing the old list
				ModelState.Clear();

				//go back to the form
				PopulateLists(sutCE);
				return View(sutCE);
			}

			//removal of existing 'UzsakytosPaslaugos' record was requested?
			if (remove != null)
			{
				//filter out 'UzsakytosPaslaugos' record having in-list-id the same as the given one
				sutCE.Sutartis =
					sutCE
						.Sutartis
						.Where(it => it.InListId != remove.Value)
						.ToList();

				//make sure @Html helper is not reusing old model state containing the old list
				ModelState.Clear();

				//go back to the form
				PopulateLists(sutCE);
				return View(sutCE);
			}

			//save of the form data was requested?
			if (save != null)
			{
				//check for attemps to create duplicate 'UzsakytaPaslauga'records
				for (var i = 0; i < sutCE.Sutartis.Count - 1; i++)
				{
					var refUp = sutCE.Sutartis[i];

					for (var j = i + 1; j < sutCE.Sutartis.Count; j++)
					{
						var testUp = sutCE.Sutartis[j];

						if (testUp.Nr == refUp.Nr)
							ModelState.AddModelError($"Sutartis[{j}].Paslauga", "Duplicate of another added service.");
					}
				}

				//form field validation passed?
				if (ModelState.IsValid)
				{
					//create new 'Sutartis'
					sutCE.UzsakytosPaslaugos.InListId = UzsakytaPaslaugaF2Repo.InsertUzsakytaPaslauga(sutCE);

					//create new 'UzsakytosPaslaugos' records
					foreach (var upVm in sutCE.Sutartis)
						UzsakytaPaslaugaF2Repo.InsertSutartis(sutCE.UzsakytosPaslaugos.InListId, upVm);

					//save success, go back to the entity list
					return RedirectToAction("Index");
				}
				//form field validation failed, go back to the form
				else
				{
					PopulateLists(sutCE);
					return View(sutCE);
				}
			}

			//should not reach here
			throw new Exception("Should not reach here.");
		}

		/// <summary>
		/// This is invoked when editing form is first opened in browser.
		/// </summary>
		/// <param name="id">ID of the entity to edit.</param>
		/// <returns>Editing form view.</returns>
		[HttpGet]
		public ActionResult Edit(int id)
		{
			var sutCE = UzsakytaPaslaugaF2Repo.FindUzsakytaPaslaugaCE(id);

			sutCE.Sutartis = UzsakytaPaslaugaF2Repo.ListSutartis(id);
			PopulateLists(sutCE);

			return View(sutCE);
		}

		/// <summary>
		/// This is invoked when buttons are pressed in the editing form.
		/// </summary>
		/// <param name="id">ID of the entity being edited</param>
		/// <param name="save">If not null, indicates that 'Save' button was clicked.</param>
		/// <param name="add">If not null, indicates that 'Add' button was clicked.</param>
		/// <param name="remove">If not null, indicates that 'Remove' button was clicked and contains in-list-id of the item to remove.</param>
		/// <param name="sutCE">Entity view model filled with latest data.</param>
		/// <returns>Returns editing from view or redired back to Index if save is successfull.</returns>
		[HttpPost]
		public ActionResult Edit(int id, int? save, int? add, int? remove, UzsakytaPaslaugaCE sutCE)
		{
			//addition of new 'UzsakytosPaslaugos' record was requested?
			if (add != null)
			{
				//add entry for the new record
				var up =
					new UzsakytaPaslaugaCE.SutartisM
					{
						InListId =
							sutCE.Sutartis.Count > 0 ?
							sutCE.Sutartis.Max(it => it.InListId) + 1 :
							0,

						//Paslauga = null,
						//Kiekis = 0,
						Kaina = 0
					};
				sutCE.Sutartis.Add(up);

				//make sure @Html helper is not reusing old model state containing the old list
				ModelState.Clear();

				//go back to the form
				PopulateLists(sutCE);
				return View(sutCE);
			}

			//removal of existing 'UzsakytosPaslaugos' record was requested?
			if (remove != null)
			{
				//filter out 'UzsakytosPaslaugos' record having in-list-id the same as the given one
				sutCE.Sutartis =
					sutCE
						.Sutartis
						.Where(it => it.InListId != remove.Value)
						.ToList();

				//make sure @Html helper is not reusing old model state containing the old list
				ModelState.Clear();

				//go back to the form
				PopulateLists(sutCE);
				return View(sutCE);
			}

			//save of the form data was requested?
			if (save != null)
			{
				//check for attemps to create duplicate 'UzsakytaPaslauga'records
				for (var i = 0; i < sutCE.Sutartis.Count - 1; i++)
				{
					var refUp = sutCE.Sutartis[i];

					for (var j = i + 1; j < sutCE.Sutartis.Count; j++)
					{
						var testUp = sutCE.Sutartis[j];

						if (testUp.InListId == refUp.InListId)
							ModelState.AddModelError($"Sutartis[{j}].Paslauga", "Duplicate of another added service.");
					}
				}

				//form field validation passed?
				if (ModelState.IsValid)
				{
					//update 'Sutartis'
					UzsakytaPaslaugaF2Repo.UpdateUzsakytaPaslauga(sutCE);

					//delete all old 'UzsakytosPaslaugos' records
					UzsakytaPaslaugaF2Repo.DeleteSutartis(sutCE.UzsakytosPaslaugos.InListId);

					//create new 'UzsakytosPaslaugos' records
					foreach (var upVm in sutCE.Sutartis)
						UzsakytaPaslaugaF2Repo.InsertSutartis(sutCE.UzsakytosPaslaugos.InListId, upVm);

					//save success, go back to the entity list
					return RedirectToAction("Index");
				}
				//form field validation failed, go back to the form
				else
				{
					PopulateLists(sutCE);
					return View(sutCE);
				}
			}

			//should not reach here
			throw new Exception("Should not reach here.");
		}

		/// <summary>
		/// This is invoked when deletion form is first opened in browser.
		/// </summary>
		/// <param name="id">ID of the entity to delete.</param>
		/// <returns>Deletion form view.</returns>
		[HttpGet]
		public ActionResult Delete(int id)
		{
			var sutCE = UzsakytaPaslaugaF2Repo.FindUzsakytaPaslaugaCE(id);
			return View(sutCE);
		}

		/// <summary>
		/// This is invoked when deletion is confirmed in deletion form
		/// </summary>
		/// <param name="id">ID of the entity to delete.</param>
		/// <returns>Deletion form view on error, redirects to Index on success.</returns>
		[HttpPost]
		public ActionResult DeleteConfirm(int id)
		{
			//load 'Sutartis'
			var sutCE = UzsakytaPaslaugaF2Repo.FindUzsakytaPaslaugaCE(id);

			//'Sutartis' is in the state where deletion is permitted?
			if (sutCE.UzsakytosPaslaugos.FkPaslauga == 1 || sutCE.UzsakytosPaslaugos.FkPaslauga == 3)
			{
				//delete the entity
				UzsakytaPaslaugaF2Repo.DeleteUzsakytaPaslauga(id);
				UzsakytaPaslaugaF2Repo.DeleteSutartis(id);

				//redired to list form
				return RedirectToAction("Index");
			}
			//'Sutartis' is in state where deletion is not permitted
			else
			{
				//enable explanatory message and show delete form
				ViewData["deletionNotPermitted"] = true;
				return View("Delete", sutCE);
			}
		}

		/// <summary>
		/// Populates select lists used to render drop down controls.
		/// </summary>
		/// <param name="sutCE">'Sutartis' view model to append to.</param>
		private void PopulateLists(UzsakytaPaslaugaCE sutCE)
		{
			//load entities for the select lists
			var automobiliai = UzsakytaPaslaugaF2Repo.ListUzsakytaPaslauga();
			var VatNeAutomobiliai = SutartisRepo.List();
			//var darbuotojai = PaslaugosKainaRepo.List();
			//var busenos = SutartisF2Repo.ListSutartiesBusena();
			/*var klientai = KlientasRepo.List();
			var aiksteles = AiksteleRepo.List();*/

			//build select lists
			sutCE.Lists.Paslaugos =
				automobiliai
					.Select(it =>
					{
						return
							new SelectListItem
							{
								Value = Convert.ToString(it.Nr),
								Text = $"{it.Nr} - {it.Laikas} {it.Data}"
							};
					})
					.ToList();


			sutCE.Lists.Sutartis =
				VatNeAutomobiliai
					.Select(it =>
					{
						return
							new SelectListItem
							{
								Value = Convert.ToString(it.Id),
								Text = $"{it.Id}. {it.Data} {it.Laikas}"
							};
					})
					.ToList();

			//build select list for 'UzsakytosPaslaugos'
			/*{
				//initialize the destination list
				sutCE.Lists.Sutartis = new List<SelectListItem>();

				//load 'Paslaugos' to use for item groups
				var paslaugos = SutartisRepo.List();

				//create select list items from 'PaslauguKainos' related to each 'Paslaugos'
				foreach (var paslauga in paslaugos)
				{
					//create list item group for current 'Paslaugos' entity
					var itemGrp = new SelectListGroup() { Name = paslauga.Laikas };

					//load related 'PaslauguKaina' entities
					var kainos = DarbuotojasRepo.LoadForSutartis(paslauga.Id);

					//build list items for the group
					foreach (var kaina in kainos)
					{
						var sle =
							new SelectListItem
							{
								Value =
									//we use JSON here to make serialization/deserializaton of composite key more convenient
									JsonConvert.SerializeObject(new
									{
										FkPaslauga = paslauga.Id,
										GaliojaNuo = kaina.Pavarde
									}),
								Text = $"{paslauga.Data} VARDAS: {kaina.Vardas} PAVARDE: {kaina.Pavarde}",
								Group = itemGrp
							};
						sutCE.Lists.Paslaugos.Add(sle);
					}
				}
			}*/
		}

	}
}
