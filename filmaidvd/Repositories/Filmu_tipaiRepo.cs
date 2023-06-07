using Org.Ktu.Isk.P175B602.Autonuoma;
using MySql.Data.MySqlClient;
using Autonuoma.Models;

namespace Autonuoma.Repositories
{

	public class Filmu_tipaiRepo
	{
		public static List<Filmu_tipai> List()
		{
			string query = $@"SELECT * FROM `{Config.TblPrefix}filmu_tipai` ORDER BY id_Filmu_tipai ASC";
			var drc = Sql.Query(query);

			var result =
				Sql.MapAll<Filmu_tipai>(drc, (dre, t) => {
					t.Id = dre.From<int>("id_Filmu_tipai");
					t.Pavadinimas = dre.From<string>("name");
				});

			return result;
		}

		public static Filmu_tipai Find(int id)
		{
			var query = $@"SELECT * FROM `{Config.TblPrefix}filmu_tipai` WHERE id_Filmu_tipai=?id_Filmu_tipai";
			var drc =
				Sql.Query(query, args => {
					args.Add("?id_Filmu_tipai", id);
				});

			var result =
				Sql.MapOne<Filmu_tipai>(drc, (dre, t) => {
					t.Id = dre.From<int>("id_Filmu_tipai");
					t.Pavadinimas = dre.From<string>("name");
				});

			return result;
		}

		public static void Update(Filmu_tipai marke)
		{
			var query =
				$@"UPDATE `{Config.TblPrefix}filmu_tipai` 
			SET 
				name=?pavadinimas 
			WHERE 
				id_Filmu_tipai=?id";

			Sql.Update(query, args => {
				args.Add("?pavadinimas", marke.Pavadinimas);
				args.Add("?id", marke.Id);
			});
		}

		public static void Insert(Filmu_tipai marke)
		{
			var query = $@"INSERT INTO `{Config.TblPrefix}filmu_tipai` ( name ) VALUES ( ?pavadinimas )";
			Sql.Insert(query, args => {
				args.Add("?pavadinimas", marke.Pavadinimas);
			});
		}

		public static void Delete(int id)
		{
			var query = $@"DELETE FROM `{Config.TblPrefix}filmu_tipai` where id_Filmu_tipai=?id";
			Sql.Delete(query, args => {
				args.Add("?id", id);
			});
		}
	}
}
