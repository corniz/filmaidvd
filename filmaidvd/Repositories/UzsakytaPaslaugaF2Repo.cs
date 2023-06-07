using Org.Ktu.Isk.P175B602.Autonuoma;
using MySql.Data.MySqlClient;
using Autonuoma.Models;
using Newtonsoft.Json;
using static Autonuoma.Models.UzsakytaPaslaugaCE;
using System;

namespace Autonuoma.Repositories
{
	public class UzsakytaPaslaugaF2Repo
	{
		public static List<UzsakytaPaslaugaL> ListUzsakytaPaslauga()
		{
			var query =
				$@"SELECT
				s.id_Uzsakyta_paslauga,
				s.data as data,
				d.laikas as laikas,
				n.kaina as kaina
			FROM
				`{Config.TblPrefix}uzsakyta_paslauga` s
				LEFT JOIN `{Config.TblPrefix}sutartis` d ON s.fk_SutartisSutarties_id=d.Sutarties_id
				LEFT JOIN `{Config.TblPrefix}paslaugos_kaina` n ON s.fk_Paslaugos_kainaid_Paslaugos_kaina=n.id_Paslaugos_kaina
			ORDER BY s.id_Uzsakyta_paslauga DESC
			";

			var drc = Sql.Query(query);

			var result =
				Sql.MapAll<UzsakytaPaslaugaL>(drc, (dre, t) => {
					t.Nr = dre.From<int>("id_Uzsakyta_paslauga");
					t.Data= dre.From<string>("data");
					t.Laikas = dre.From<string>("laikas");
					t.PaslaugosKaina = dre.From<double>("kaina");
				});

			return result;
		}

		public static UzsakytaPaslaugaCE FindUzsakytaPaslaugaCE(int nr)
		{
			var query = $@"SELECT * FROM `{Config.TblPrefix}uzsakyta_paslauga` WHERE id_Uzsakyta_paslauga=?nr";
			var drc =
				Sql.Query(query, args => {
					args.Add("?nr", nr);
				});

			var result =
				Sql.MapOne<UzsakytaPaslaugaCE>(drc, (dre, t) => {
					//make a shortcut
					var sut = t.UzsakytosPaslaugos;

					//
					sut.InListId = dre.From<int>("id_Uzsakyta_paslauga");
					sut.Kaina = dre.From<double>("kaina");
					sut.Data = dre.From<string>("data");
					sut.Kiekis = dre.From<int>("kiekis");
					sut.FkPaslauga = dre.From<int>("fk_Paslaugos_kainaid_Paslaugos_kaina");
					sut.FkSutartis = dre.From<int>("fk_SutartisSutarties_id");
				});

			return result;
		}

		public static int InsertUzsakytaPaslauga(UzsakytaPaslaugaCE sutCE)
		{
			var query =
				$@"INSERT INTO `{Config.TblPrefix}uzsakyta_paslauga`
			(
				`kiekis`,
				`kaina`,
				`data`,
				`id_Uzsakyta_paslauga`,
				`fk_SutartisSutarties_id`,
				`fk_Paslaugos_kainaid_Paslaugos_kaina`
			)
			VALUES(
				?sutdata,
				?nuomdata,
				?plgrlaikas,
				?fkgrlaikas,
				?prrida,
				?glrida
			)";

			var nr =
				Sql.Insert(query, args => {
					//make a shortcut
					var sut = sutCE.UzsakytosPaslaugos;

					//
					args.Add("?sutdata", sut.Kiekis);
					args.Add("?nuomdata", sut.Kaina);
					args.Add("?plgrlaikas", sut.Data);
					args.Add("?fkgrlaikas", sut.InListId);
					args.Add("?prrida", sut.FkSutartis);
					args.Add("?glrida", sut.FkPaslauga);
				});

			return (int)nr;
		}

		public static void UpdateUzsakytaPaslauga(UzsakytaPaslaugaCE sutCE)
		{
			var query =
				$@"UPDATE `{Config.TblPrefix}uzsakyta_paslauga`
			SET
				`kiekis` = ?sutdata,
				`kaina` = ?nuomdata,
				`data` = ?plgrlaikas,
				`id_Uzsakyta_paslauga` = ?fkgrlaikas,
				`fk_SutartisSutarties_id` = ?prrida,
				`fk_Paslaugos_kainaid_Paslaugos_kaina` = ?glrida
			WHERE id_Uzsakyta_paslauga=?nr";

			Sql.Update(query, args => {
				//make a shortcut
				var sut = sutCE.UzsakytosPaslaugos;

				//
				args.Add("?sutdata", sut.Kiekis);
				args.Add("?nuomdata", sut.Kaina);
				args.Add("?plgrlaikas", sut.Data);
				args.Add("?fkgrlaikas", sut.InListId);
				args.Add("?prrida", sut.FkSutartis);
				args.Add("?glrida", sut.FkPaslauga);

				args.Add("?nr", sut.InListId);
			});
		}

		public static void DeleteUzsakytaPaslauga(int nr)
		{
			var query = $@"DELETE FROM `{Config.TblPrefix}uzsakyta_paslauga` where id_Uzsakyta_paslauga=?nr";
			Sql.Delete(query, args => {
				args.Add("?nr", nr);
			});
		}

		public static List<UzsakytaPaslaugaCE.SutartisM> ListSutartis(int sutartisId)
		{
			var query =
				$@"SELECT *
			FROM `{Config.TblPrefix}sutartis`
			WHERE fk_Darbuotojasdarbuotojo_nr = ?darbuotojo_nr
			ORDER BY fk_Kliento_priv_infoasmens_kodas ASC";

			var drc =
				Sql.Query(query, args => {
					args.Add("?darbuotojo_nr", sutartisId);
				});

			var result =
				Sql.MapAll<UzsakytaPaslaugaCE.SutartisM>(drc, (dre, t) => {
					t.Nr = dre.From<int>("Sutarties_id"); // gali but s arba S
					t.SutartiesData = dre.From<string>("sutarties_data");
					t.Galiojimas = dre.From<string>("garantinio_galiojimas");
					t.FkDarbuotojas = dre.From<int>("fk_Darbuotojasdarbuotojo_nr");
					t.FkKlientas = dre.From<Int64>("fk_Kliento_priv_infoasmens_kodas");
						//we use JSON here to make serialization/deserializaton of composite key more convenient
					/*JsonConvert.SerializeObject(new
					{
						FkPaslauga = dre.From<Int32>("fk_Kliento_priv_infoasmens_kodas"),
						GaliojaNuo = dre.From<DateTime>("fk_kaina_galioja_nuo")
					});*/
					t.Laikas = dre.From<string>("laikas");
					t.Kaina = dre.From<double>("kaina");
				});

			for (int i = 0; i < result.Count; i++)
				result[i].InListId = i;
			return result;
		}

		public static void InsertSutartis(int sutartisId, UzsakytaPaslaugaCE.SutartisM up)
		{
			//deserialize 'Paslauga' foreign keys from 'UzsakytaPaslauga' view model key
			var fks =
				JsonConvert.DeserializeAnonymousType(
					up.Paslauga, //??????
								 //this creates object of correct shape that is filled in by the JSON deserializer
					new
					{
						fkdarbuotojas = 55,
						FkKlientas = 30309573397
					}
				) ;

			//
			var query =
				$@"INSERT INTO `{Config.TblPrefix}sutartis`
				(
					Sutarties_id,
					fk_Darbuotojasdarbuotojo_nr,
					fk_Kliento_priv_infoasmens_kodas,
					sutarties_data,
					garantinio_galiojimas,
					laikas,
					kaina
				)
				VALUES(
					?sutartis,
					?fk_sutartis,
					?fk_paslauga,
					?dt,
					?galioja_nuo,
					?kiekis,
					?kaina
				)";

			Sql.Insert(query, args => {
				args.Add("?sutartis", sutartisId);
				args.Add("?fk_sutartis", fks.fkdarbuotojas);
				args.Add("?fk_paslauga", fks.FkKlientas);
				args.Add("?dt", up.SutartiesData);
				args.Add("?galioja_nuo", up.Galiojimas);
				args.Add("?kiekis", up.Laikas);
				args.Add("?kaina", up.Kaina);
			});
		}

		public static void DeleteSutartis(int sutartis)
		{
			var query =
				$@"DELETE FROM a
			USING `{Config.TblPrefix}sutartis` as a
			WHERE a.Sutarties_id=?fkid";

			Sql.Delete(query, args => {
				args.Add("?fkid", sutartis);
			});
		}
	}
}
