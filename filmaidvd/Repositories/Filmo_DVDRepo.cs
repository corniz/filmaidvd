using Org.Ktu.Isk.P175B602.Autonuoma;
using MySql.Data.MySqlClient;
using Autonuoma.Models;

namespace Autonuoma.Repositories
{
	public class Filmo_DVDRepo
	{
		public static List<Filmo_DVDL> ListFilmo_DVD()
		{
			var query =
				$@"SELECT
				a.dvd_nr,
				a.kurejai,
				b.name AS zanras,
				m.name AS apribojimai
			FROM
				{Config.TblPrefix}filmo_dvd a
				LEFT JOIN `{Config.TblPrefix}filmu_tipai` b ON b.id_Filmu_tipai = a.zanras
				LEFT JOIN `{Config.TblPrefix}amziaus_apribojimai` m ON m.id_Amziaus_apribojimai = a.amziaus_ribojimai			
			ORDER BY a.dvd_nr ASC";
			//LEFT JOIN `{Config.TblPrefix}markes` mm ON mm.id = m.fk_marke
			// zanras arba filmu_tipai
			var drc = Sql.Query(query);

			var result =
				Sql.MapAll<Filmo_DVDL>(drc, (dre, t) => {
					t.Id = dre.From<int>("dvd_nr");
					t.Kurejai = dre.From<string>("kurejai");
					t.Zanras = dre.From<string>("zanras");
					t.Apribojimai = dre.From<string>("apribojimai");
				});

			return result;
		}

		public static Filmo_DVDCE FindFilmo_DVDCE(int id)
		{
			var query = $@"SELECT * FROM `{Config.TblPrefix}filmo_dvd` WHERE dvd_nr=?id";

			var drc =
				Sql.Query(query, args => {
					args.Add("?id", id);
				});

			var result =
				Sql.MapOne<Filmo_DVDCE>(drc, (dre, t) => {
					//make a shortcut
					var auto = t.Filmo_DVD;

					//
					auto.Id = dre.From<int>("dvd_nr");
					auto.Verte = dre.From<double>("verte");
					auto.Aktoriai = dre.From<string>("aktoriai");
					auto.Ivertinimas = dre.From<int>("ivertinimas");
					auto.Kurejai = dre.From<string>("kurejai");
					auto.Aprasymas = dre.From<string>("filmo_aprasymas");
					auto.Ilgumas = dre.From<int>("filmo_ilgumas");
					auto.Kalba = dre.From<string>("filmo_kalba");
					auto.Zanras = dre.From<int>("zanras"); //?
					auto.Apribojimai = dre.From<int>("amziaus_ribojimai"); //?
				});

			return result;
		}

		public static void InsertFilmo_DVD(Filmo_DVDCE autoCE)
		{
			var query =
				$@"INSERT INTO `{Config.TblPrefix}filmo_dvd`
			(
				`verte`,
				`aktoriai`,
				`ivertinimas`,
				`kurejai`,
				`filmo_aprasymas`,
				`filmo_ilgumas`,
				`filmo_kalba`,
				`zanras`,
				`amziaus_ribojimai`
			)
			VALUES (
				?pag_data,
				?rida,
				?radijas,
				?grotuvas,
				?kond,
				?viet_sk,
				?reg_dt,
				?verte,
				?pav_deze
			)";

			Sql.Insert(query, args => {
				//make a shortcut
				var auto = autoCE.Filmo_DVD;

				//
				args.Add("?pag_data", auto.Verte);
				args.Add("?rida", auto.Aktoriai);
				args.Add("?radijas", auto.Ivertinimas);
				args.Add("?grotuvas", auto.Kurejai);
				args.Add("?kond", auto.Aprasymas);
				args.Add("?viet_sk", auto.Ilgumas);
				args.Add("?reg_dt", auto.Kalba);
				args.Add("?verte", auto.Zanras);
				args.Add("?pav_deze", auto.Apribojimai);
			});
		}

		public static void UpdateFilmo_DVD(Filmo_DVDCE autoCE)
		{
			var query =
				$@"UPDATE `{Config.TblPrefix}filmo_dvd`
			SET				
				`verte` = ?pag_data,
				`aktoriai` = ?rida,
				`ivertinimas` = ?radijas,
				`kurejai` = ?grotuvas,
				`filmo_aprasymas` = ?kond,
				`filmo_ilgumas` = ?viet_sk,
				`filmo_kalba` = ?reg_dt,
				`zanras` = ?verte,
				`amziaus_ribojimai` = ?pav_deze
			WHERE
				dvd_nr=?id";

			Sql.Update(query, args => {
				//make a shortcut
				var auto = autoCE.Filmo_DVD;

				//
				args.Add("?pag_data", auto.Verte);
				args.Add("?rida", auto.Aktoriai);
				args.Add("?radijas", auto.Ivertinimas);
				args.Add("?grotuvas", auto.Kurejai);
				args.Add("?kond", auto.Aprasymas);
				args.Add("?viet_sk", auto.Ilgumas);
				args.Add("?reg_dt", auto.Kalba);
				args.Add("?verte", auto.Zanras);
				args.Add("?pav_deze", auto.Apribojimai);

				args.Add("?id", auto.Id);
			});
		}

		public static void DeleteFilmo_DVD(int id)
		{
			var query = $@"DELETE FROM `{Config.TblPrefix}filmo_dvd` WHERE dvd_nr=?id";
			Sql.Delete(query, args => {
				args.Add("?id", id);
			});
		}

		public static List<Zanras> ListZanras()
		{
			var query = $@"SELECT * FROM `{Config.TblPrefix}filmu_tipai` ORDER BY id_Filmu_tipai ASC";
			var drc = Sql.Query(query);

			var result =
				Sql.MapAll<Zanras>(drc, (dre, t) => {
					t.Id = dre.From<int>("id_Filmu_tipai");
					t.Pavadinimas = dre.From<string>("name");
				});

			return result;
		}

		public static List<Amziaus_apribojimai> ListAmziaus_apribojimai()
		{
			var query = $@"SELECT * FROM `{Config.TblPrefix}amziaus_apribojimai` ORDER BY id_Amziaus_apribojimai ASC";
			var drc = Sql.Query(query);

			var result =
				Sql.MapAll<Amziaus_apribojimai>(drc, (dre, t) => {
					t.Id = dre.From<int>("id_Amziaus_apribojimai");
					t.Pavadinimas = dre.From<string>("name");
				});

			return result;
		}
	}
}
