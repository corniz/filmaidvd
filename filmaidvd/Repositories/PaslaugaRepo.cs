using Autonuoma.Models;
using Org.Ktu.Isk.P175B602.Autonuoma;

namespace Autonuoma.Repositories
{
	public class PaslaugaRepo
	{
		public static List<Paslauga> LoadForReport(int id)
		{
			var query =
				$@"SELECT
				pk.pavadinimas,
				pk.aprasymas,
				pk.id_Paslauga
			FROM
				`{Config.TblPrefix}paslauga` as pk
				LEFT JOIN `{Config.TblPrefix}paslaugos_kaina` up
					ON up.fk_Paslaugaid_Paslauga=pk.id_Paslauga
			WHERE pk.id_Paslauga=?id
			GROUP BY
				pk.pavadinimas,
				pk.aprasymas,
				pk.id_Paslauga
			ORDER BY pk.id_Paslauga ASC";

			var drc =
				Sql.Query(query, args => {
					args.Add("?id", id);
				});

			var result =
				Sql.MapAll<Paslauga>(drc, (dre, t) => {
					t.Id = dre.From<int>("id_Paslauga");
					t.Pavadinimas = dre.From<string>("pavadinimas");
					t.Aprasymas = dre.From<string>("aprasymas");
				});
			for (int i = 0; i < result.Count; i++)
				result[i].InListId = i;

			return result;
		}
	}
}
