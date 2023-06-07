using Org.Ktu.Isk.P175B602.Autonuoma;
using MySql.Data.MySqlClient;
using Autonuoma.Models;

namespace Autonuoma.Repositories
{

	public class Amziaus_apribojimaiRepo
	{
		public static List<Amziaus_apribojimai> List()
		{
			string query = $@"SELECT * FROM `{Config.TblPrefix}amziaus_apribojimai` ORDER BY id_Amziaus_apribojimai ASC";
			var drc = Sql.Query(query);

			var result =
				Sql.MapAll<Amziaus_apribojimai>(drc, (dre, t) => {
					t.Id = dre.From<int>("id_Amziaus_apribojimai");
					t.Pavadinimas = dre.From<string>("name");
				});

			return result;
		}

		public static Amziaus_apribojimai Find(int id)
		{
			var query = $@"SELECT * FROM `{Config.TblPrefix}amziaus_apribojimai` WHERE id_Amziaus_apribojimai=?id_Amziaus_apribojimai";
			var drc =
				Sql.Query(query, args => {
					args.Add("?id_Amziaus_apribojimai", id);
				});

			var result =
				Sql.MapOne<Amziaus_apribojimai>(drc, (dre, t) => {
					t.Id = dre.From<int>("id_Amziaus_apribojimai");
					t.Pavadinimas = dre.From<string>("name");
				});

			return result;
		}

		public static void Update(Amziaus_apribojimai marke)
		{
			var query =
				$@"UPDATE `{Config.TblPrefix}amziaus_apribojimai` 
			SET 
				name=?pavadinimas 
			WHERE 
				id_Amziaus_apribojimai=?id";

			Sql.Update(query, args => {
				args.Add("?pavadinimas", marke.Pavadinimas);
				args.Add("?id", marke.Id);
			});
		}

		public static void Insert(Amziaus_apribojimai marke)
		{
			var query = $@"INSERT INTO `{Config.TblPrefix}amziaus_apribojimai` ( name ) VALUES ( ?pavadinimas )";
			Sql.Insert(query, args => {
				args.Add("?pavadinimas", marke.Pavadinimas);
			});
		}

		public static void Delete(int id)
		{
			var query = $@"DELETE FROM `{Config.TblPrefix}amziaus_apribojimai` where id_Amziaus_apribojimai=?id";
			Sql.Delete(query, args => {
				args.Add("?id", id);
			});
		}
	}
}
